import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { SearchFieldComponent } from '../../components/search-field/search-field.component';
import { LibraryFilteringService } from '../library-filtering.service';
import { ButtonToggle } from '../../components/button-toggle-group/button-toggle-group.component';
import { ApiManagerService } from '../../services/api-manager.service';
import { map } from 'rxjs/operators';

@Component({
    selector: 'app-library-sidebar',
    templateUrl: './library-sidebar.component.html',
    styleUrls: ['./library-sidebar.component.scss']
})
export class LibrarySidebarComponent implements AfterViewInit, OnInit {
    @ViewChild(SearchFieldComponent, { static: false })
    private searchField: SearchFieldComponent;

    sourceFormatButtons: ButtonToggle[] = [
        { value: '', name: 'All', checked: true }
    ];

    constructor(
        private libraryFilteringService: LibraryFilteringService,
        private apiManagerService: ApiManagerService
    ) {
        this.apiManagerService.getSourceFormatsSubject
            .pipe(
                map(newSourceFormat => ({
                    value: newSourceFormat.sourceFormatId.toString(),
                    name: newSourceFormat.sourceFormatName
                }))
            )
            .subscribe(newSourceFormats =>
                this.sourceFormatButtons.push(newSourceFormats)
            );

        this.apiManagerService.getSourcesSubject.subscribe(() => {
            this.searchField.searchControl.reset();
        });
    }

    updateFilter(
        filterName,
        filterValue,
        filterComparer,
        filterComparisonValue
    ) {
        this.libraryFilteringService.updateFilters({
            name: filterName,
            value: filterValue,
            comparer: filterComparer,
            comparisonValue: filterComparisonValue
        });
    }

    ngOnInit(): void {}

    ngAfterViewInit(): void {
    }
}
