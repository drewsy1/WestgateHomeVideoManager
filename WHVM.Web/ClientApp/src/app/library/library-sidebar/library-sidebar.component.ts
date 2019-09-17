import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { SearchFieldComponent } from '../../components/search-field/search-field.component';
import { LibraryService } from '../library.service';

import { Subject } from 'rxjs';
import { FilterButtonGroupComponent } from './filter-button-group/filter-button-group.component';

@Component({
    selector: 'app-library-sidebar',
    templateUrl: './library-sidebar.component.html',
    styleUrls: ['./library-sidebar.component.scss']
})
export class LibrarySidebarComponent implements AfterViewInit, OnInit {
    @ViewChild(SearchFieldComponent, { static: false })
    private searchField: SearchFieldComponent;

    @ViewChild(FilterButtonGroupComponent, { static: false })
    private sourceFormatFilterButtons: FilterButtonGroupComponent;

    filterSourcesSearchSubject: Subject<string> = this.libraryFilteringService
        .filterSourcesSearchSubject;

    filterSourcesFormatSubject: Subject<string> = this.libraryFilteringService
        .filterSourcesFormatSubject;

    constructor(private libraryFilteringService: LibraryService) {
    }

    ngOnInit(): void {
    }

    ngAfterViewInit(): void {
    }
}
