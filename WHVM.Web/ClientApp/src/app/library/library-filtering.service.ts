import { Injectable } from '@angular/core';
import { ISource } from '../../interfaces/ISource';
import { ApiManagerService } from '../services/api-manager.service';
import { combineLatest, Observable, Subject } from 'rxjs';
import { delay, map, startWith } from 'rxjs/operators';
import { intersection } from 'lodash';

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
export class LibraryFilteringService {
    allSources: ISource[] = [];
    sourcesFiltered: Observable<ISource[]>;

    filterSourcesSearchSubject: Subject<string> = new Subject<string>();
    filterSourcesSearch: Filter = {
        comparer: 'match',
        comparisonField: 'sourceName',
        filtered: this.filterSourcesSearchSubject.pipe(
            startWith(null),
            map((searchString: string | null) =>
                searchString
                    ? this.applyFilter(this.filterSourcesSearch, searchString)
                    : this.allSources.slice()
            )
        )
    };

    filterSourcesFormatSubject: Subject<string> = new Subject<string>();
    filterSourcesFormat: Filter = {
        comparer: '===',
        comparisonField: 'sourceFormatId',
        filtered: this.filterSourcesFormatSubject.pipe(
            startWith(null),
            map((searchString: string | null) =>
                searchString
                    ? this.applyFilter(this.filterSourcesFormat, searchString)
                    : this.allSources.slice()
            )
        )
    };

    applyFilter(filter: Filter, filterValue?: any) {
        return this.allSources.filter((source: ISource) =>
            filterOperations[filter.comparer](
                filterValue,
                source[filter.comparisonField]
            )
        );
    }

    constructor(public apiManagerService: ApiManagerService) {
        this.apiManagerService.getSourcesSubject.subscribe(
            newSources => (this.allSources = newSources)
        );

        const activeFilteredSources = [this.filterSourcesSearch.filtered, this.filterSourcesFormat.filtered];
        this.sourcesFiltered = combineLatest(activeFilteredSources).pipe(
            delay(0),
            map(source => intersection(...source))
        );
    }
}
