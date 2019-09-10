import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { SearchFieldComponent } from '../../components/search-field/search-field.component';
import { LibraryFilteringService } from '../library-filtering.service';
import {
    ButtonToggle,
    ButtonToggleGroupComponent
} from '../../components/button-toggle-group/button-toggle-group.component';
import { ApiManagerService } from '../../services/api-manager.service';
import { map } from 'rxjs/operators';
import { union } from 'lodash';
import { Subject } from 'rxjs';

@Component({
    selector: 'app-library-sidebar',
    templateUrl: './library-sidebar.component.html',
    styleUrls: ['./library-sidebar.component.scss']
})
export class LibrarySidebarComponent implements AfterViewInit, OnInit {
    @ViewChild(SearchFieldComponent, { static: false })
    private searchField: SearchFieldComponent;

    @ViewChild(ButtonToggleGroupComponent, { static: false })
    private buttonToggleGroup: ButtonToggleGroupComponent;

    sourceFormatButtons: ButtonToggle[] = [
        { value: '', name: 'All', checked: true }
    ];

    filterSourcesSearchSubject: Subject<string> = this.libraryFilteringService
        .filterSourcesSearchSubject;

    filterSourcesFormatSubject: Subject<string> = this.libraryFilteringService
        .filterSourcesFormatSubject;

    constructor(
        private libraryFilteringService: LibraryFilteringService,
        private apiManagerService: ApiManagerService
    ) {
        this.apiManagerService.getSourceFormatsSubject
            .pipe(
                map(x =>
                    x.map(
                        newSourceFormat =>
                            ({
                                value: newSourceFormat.sourceFormatId.toString(),
                                name: newSourceFormat.sourceFormatName
                            } as ButtonToggle)
                    )
                )
            )
            .subscribe(
                newSourceFormats =>
                    (this.sourceFormatButtons = union(
                        this.sourceFormatButtons,
                        newSourceFormats
                    ))
            );
    }

    ngOnInit(): void {
    }

    ngAfterViewInit(): void {
        this.apiManagerService.getSourcesSubject.subscribe(() => {
            this.searchField.searchControl.reset();
            this.buttonToggleGroup.valueChanges.emit('');
        });
    }
}
