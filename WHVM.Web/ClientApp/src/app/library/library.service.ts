import { Injectable } from '@angular/core';
import { ApiManagerService } from '../services/api-manager.service';
import { Subject } from 'rxjs';

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
    loadedContent = false;
    loadedContent$: Subject<boolean> = new Subject<boolean>();

    static updateFilters(
        updatedFilters: IFilter[],
        activeFilters: Subject<IFilter[]>
    ) {
        activeFilters.next(updatedFilters);
    }

    static updateFilteredItems(
        newFilters: IFilter[],
        defaultValues
    ): IFilter[] {
        return newFilters.map(filter =>
            Object.assign(filter, {
                filtered: filter.value
                    ? this.applyFilter(filter, defaultValues, filter.value)
                    : defaultValues.slice()
            })
        );
    }

    static applyFilter(filter: IFilter, defaultValues, filterValue?: any) {
        return defaultValues.filter((filterable: any) =>
            typeof filter.comparisonField === 'string'
                ? filterable[filter.comparisonField] &&
                filterOperations[filter.comparer](
                    filterValue,
                    filterable[filter.comparisonField]
                )
                : filter.comparisonField.some(
                cf =>
                    filterable[cf] &&
                    filterOperations[filter.comparer](
                        filterValue,
                        filterable[cf]
                    )
                )
        );
    }



    constructor(public apiManagerService: ApiManagerService) {
        this.loadedContent$.subscribe(() => this.loadedContent = true);
    }
}
