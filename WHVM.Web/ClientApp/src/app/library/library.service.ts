import { Injectable } from '@angular/core';
import { ISource } from '../../interfaces/ISource';
import { ApiManagerService } from '../services/api-manager.service';
import { combineLatest, Observable, Subject } from 'rxjs';
import { map, startWith, tap } from 'rxjs/operators';
import { intersectionBy } from 'lodash';
import { IFilter } from '../../interfaces/IFilter';
import moment from 'moment';

const filterOperations = {
    match: (value, comparisonValue) =>
        !!comparisonValue.toLowerCase().match(value.toLowerCase()),
    '===': (value, comparisonValue) =>
        moment.isMoment(value) && moment.isMoment(comparisonValue)
            ? value.isSame(comparisonValue)
            : (typeof comparisonValue === 'number'
            ? parseInt(value, 10)
            : value) === comparisonValue,
    '<=': (value, comparisonValue) =>
        moment.isMoment(value) && moment.isMoment(comparisonValue)
            ? comparisonValue.isSameOrBefore(value)
            : (typeof comparisonValue === 'number'
            ? parseInt(value, 10)
            : value) <= comparisonValue,
    '>=': (value, comparisonValue) =>
        moment.isMoment(value) && moment.isMoment(comparisonValue)
            ? comparisonValue.isSameOrAfter(value)
            : (typeof comparisonValue === 'number'
            ? parseInt(value, 10)
            : value) >= comparisonValue
};

@Injectable({
    providedIn: 'root'
})
export class LibraryService {
    sourcesFiltered: Observable<ISource[]>;
    activeFilters: IFilter[];
    activeFilters$: Subject<IFilter[]> = new Subject<IFilter[]>();

    loadedContent$: Subject<boolean> = new Subject<boolean>();

    updateFilters(updatedFilters: IFilter[]) {
        this.activeFilters$.next(updatedFilters);
    }

    updateFilteredItems(newFilters: IFilter[]) {
        return newFilters.map(filter =>
            Object.assign(filter, {
                filtered: filter.value
                    ? this.applyFilter(filter, filter.value)
                    : this.apiManagerService.sources.slice()
            })
        );
    }

    applyFilter(filter: IFilter, filterValue?: any) {
        return this.apiManagerService.sources.filter((source: ISource) =>
            typeof filter.comparisonField === 'string'
                ? source[filter.comparisonField] &&
                filterOperations[filter.comparer](
                    filterValue,
                    source[filter.comparisonField]
                )
                : filter.comparisonField.some(
                cf =>
                    source[cf] &&
                    filterOperations[filter.comparer](
                        filterValue,
                        source[cf]
                    )
                )
        );
    }

    getLibraryData() {
        this.apiManagerService.getSources().subscribe();
        this.apiManagerService.getSourceFormats().subscribe();

        this.sourcesFiltered = combineLatest(
            this.activeFilters$.pipe(
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
            map(x => this.updateFilteredItems(x[0])),
            tap(x => (this.activeFilters = x)),
            map(filters =>
                intersectionBy(
                    ...filters.map(filter => filter.filtered),
                    'sourceId'
                )
            )
        );

        // const activeFilteredSources = this.activeFilters.map(x => x.filtered);
        // this.sourcesFiltered = combineLatest(...activeFilteredSources).pipe(
        //     delay(0),
        //     map(source => intersectionBy(...source, 'sourceId'))
        // );

        this.sourcesFiltered.subscribe();

        return this.sourcesFiltered;
    }

    constructor(public apiManagerService: ApiManagerService) {
    }
}
