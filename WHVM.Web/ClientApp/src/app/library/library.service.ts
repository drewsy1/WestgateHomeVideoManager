import { Injectable } from '@angular/core';
import { ISource } from '../../interfaces/ISource';
import { ApiManagerService } from '../services/api-manager.service';
import { combineLatest, Observable, Subject } from 'rxjs';
import { delay, map, startWith } from 'rxjs/operators';
import { intersectionBy } from 'lodash';

interface Filter {
    comparer: string;
    comparisonField: string;
    filtered: Observable<ISource[]>;
}

const filterOperations = {
    match: (value, comparisonValue) =>
        !!comparisonValue.toLowerCase().match(value.toLowerCase()),
    '===': (value, comparisonValue) =>
        (typeof comparisonValue === 'number' ? parseInt(value, 10) : value) ===
        comparisonValue
};

@Injectable({
    providedIn: 'root'
})
export class LibraryService {
    sourcesFiltered: Observable<ISource[]>;

    filterSourcesSearchSubject: Subject<string> = new Subject<string>();
    filterSourcesSearch: Filter = {
        comparer: 'match',
        comparisonField: 'sourceName',
        filtered: combineLatest(
            this.filterSourcesSearchSubject.pipe(startWith('')),
            this.apiManagerService.getSources().pipe(startWith(this.apiManagerService.sources))
        ).pipe(
            map(source =>
                source[0]
                    ? this.applyFilter(this.filterSourcesSearch, source[0])
                    : source[1].slice()
            )
        )
    };

    filterSourcesFormatSubject: Subject<string> = new Subject<string>();
    filterSourcesFormat: Filter = {
        comparer: '===',
        comparisonField: 'sourceFormatId',
        filtered: combineLatest(
            this.filterSourcesFormatSubject.pipe(startWith('')),
            this.apiManagerService.getSources().pipe(startWith(this.apiManagerService.sources))
        ).pipe(
            map(source =>
                source[0]
                    ? this.applyFilter(this.filterSourcesFormat, source[0])
                    : source[1].slice()
            )
        )
    };

    applyFilter(filter: Filter, filterValue?: any) {
        return this.apiManagerService.sources.filter((source: ISource) =>
            filterOperations[filter.comparer](
                filterValue,
                source[filter.comparisonField]
            )
        );
    }

    getLibraryData() {
        this.apiManagerService.getSources().subscribe();
        this.apiManagerService.getSourceFormats().subscribe();

        this.filterSourcesSearchSubject.next('');
        this.filterSourcesFormatSubject.next('');

        const activeFilteredSources = [
            this.filterSourcesSearch.filtered,
            this.filterSourcesFormat.filtered
        ];
        this.sourcesFiltered = combineLatest(activeFilteredSources).pipe(
            delay(0),
            map(source => intersectionBy(...source, 'sourceId'))
        );

        this.sourcesFiltered.subscribe();

        return this.sourcesFiltered;
    }

    constructor(public apiManagerService: ApiManagerService) {
    }
}
