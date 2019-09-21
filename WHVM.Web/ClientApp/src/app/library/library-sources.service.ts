import { Injectable } from '@angular/core';
import { combineLatest, Observable, Subject } from 'rxjs';
import { ISource } from '../../interfaces/ISource';
import { IFilter } from '../../interfaces/IFilter';
import { LibraryService } from './library.service';
import { map, startWith, tap } from 'rxjs/operators';
import { intersectionBy } from 'lodash';

@Injectable({
    providedIn: 'root'
})
export class LibrarySourcesService {
    loadedContent = this.libraryService.loadedContent;
    apiManagerService = this.libraryService.apiManagerService;
    loadedContent$ = this.libraryService.loadedContent$;
    sourcesFiltered: Observable<ISource[]>;
    activeSourceFilters: IFilter[];
    activeSourceFilters$: Subject<IFilter[]> = new Subject<IFilter[]>();

    getLibrarySourcesData() {
        this.apiManagerService.getSources().subscribe();
        this.apiManagerService.getSourceFormats().subscribe();

        this.sourcesFiltered = combineLatest(
            this.activeSourceFilters$.pipe(
                startWith([
                    {
                        comparer: 'match',
                        comparisonField: 'sourceName',
                        filtered: null
                    }
                ])
            ),
            this.apiManagerService
                .getSources()
                .pipe(startWith(this.apiManagerService.sources))
        ).pipe(
            map(x =>
                LibraryService.updateFilteredItems(
                    x[0],
                    this.apiManagerService.sources
                )
            ),
            tap(x => (this.activeSourceFilters = x)),
            map(filters =>
                intersectionBy(
                    ...filters.map(filter => filter.filtered as ISource[]),
                    'sourceId'
                )
            )
        );

        this.sourcesFiltered.subscribe();

        return this.sourcesFiltered;
    }

    constructor(private libraryService: LibraryService) {
    }
}
