import { Injectable } from '@angular/core';
import { ISource } from '../../interfaces/ISource';
import { ApiManagerService } from '../services/api-manager.service';
import { Subject } from 'rxjs';

interface Filter {
    value: any;
    comparer: string;
    comparisonValue: any;
    filteredKeys?: any;
}

const filterOperations = {
    match: (value, comparisonValue) =>
        !!comparisonValue.toLowerCase().match(value.toLowerCase()),
    '===': (value, comparisonValue) => (typeof comparisonValue === 'number' ? parseInt(value, 10) : value) === comparisonValue
};

@Injectable({
    providedIn: 'root'
})
export class LibraryFilteringService {
    sources: ISource[] = [];
    sourcesFiltered: ISource[] = [];

    filters: Map<string, Filter> = new Map<string, Filter>();

    updateFilterSubject: Subject<any> = new Subject<any>();

    applyFilter(filter: Filter) {
        if (!filter.value) {
            filter.filteredKeys = [];
            return;
        } else {
            const filteredSources = this.sources.filter(
                (source: ISource) =>
                    !filterOperations[filter.comparer](
                        filter.value,
                        source[filter.comparisonValue]
                    )
            );
            filter.filteredKeys = filteredSources.map(
                source => source.sourceId
            );
        }
    }

    combineFilters(filters: Map<string, Filter>): ISource[] {
        const rawFilters: Filter[] = [];
        filters.forEach(filter => rawFilters.push(filter));
        return rawFilters === [] || rawFilters === null
            ? this.sources
            : this.sources.filter(source => {
                const tempKeys = rawFilters.map(x => x.filteredKeys);
                const tempKeysCombined = [].concat(...tempKeys);
                const tempKeysFiltered = tempKeysCombined.filter(
                    (value, index, array) => array.indexOf(value) === index
                );
                return !tempKeysFiltered.includes(source.sourceId);
            });
    }

    updateFilters(updatedFilter: any) {
        this.filters.set(updatedFilter.name, updatedFilter);
        this.applyFilter(this.filters.get(updatedFilter.name));
        this.sourcesFiltered = this.combineFilters(this.filters);
    }

    constructor(public apiManagerService: ApiManagerService) {
        this.apiManagerService.getSourcesSubject.subscribe(
            () => (this.sources = this.apiManagerService.sources),
            null,
            () =>
                this.updateFilterSubject.subscribe(updatedFilter =>
                    this.updateFilters(updatedFilter)
                )
        );
    }
}
