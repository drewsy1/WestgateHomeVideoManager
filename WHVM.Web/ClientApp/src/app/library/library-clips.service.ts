import { Injectable } from '@angular/core';
import { combineLatest, Observable, Subject } from 'rxjs';
import { IFilter } from '../../interfaces/IFilter';
import { LibraryService } from './library.service';
import { map, startWith, tap } from 'rxjs/operators';
import { intersectionBy } from 'lodash';
import { IClip } from '../../interfaces/IClip';

@Injectable({
    providedIn: 'root'
})
export class LibraryClipsService {
    loadedContent = this.libraryService.loadedContent;
    apiManagerService = this.libraryService.apiManagerService;
    loadedContent$ = this.libraryService.loadedContent$;
    clipsFiltered: Observable<IClip[]>;
    activeClipFilters: IFilter[];
    activeClipFilters$: Subject<IFilter[]> = new Subject<IFilter[]>();

    getLibraryClipsData() {
        this.apiManagerService.getClips().subscribe();

        this.clipsFiltered = combineLatest(
            this.activeClipFilters$.pipe(
                startWith([
                    {
                        comparer: 'match',
                        comparisonField: 'clipName',
                        filtered: null
                    }
                ])
            ),
            this.apiManagerService
                .getClips()
                .pipe(startWith(this.apiManagerService.clips))
        ).pipe(
            map(x =>
                LibraryService.updateFilteredItems(
                    x[0],
                    this.apiManagerService.clips
                )
            ),
            tap(x => (this.activeClipFilters = x)),
            map(filters =>
                intersectionBy(
                    ...filters.map(filter => filter.filtered as IClip[]),
                    'clipId'
                )
            )
        );

        this.clipsFiltered.subscribe();

        return this.clipsFiltered;
    }

    constructor(private libraryService: LibraryService) {
    }
}
