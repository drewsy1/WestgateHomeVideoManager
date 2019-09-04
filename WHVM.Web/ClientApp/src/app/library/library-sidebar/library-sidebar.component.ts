import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { SearchFieldComponent } from '../../components/search-field/search-field.component';
import { Source } from '../../../interfaces/Source';
import { LibraryFilteringService } from '../library-filtering.service';
import { ButtonToggle } from '../../components/button-toggle-group/button-toggle-group.component';
import { SourceFormat } from '../../../interfaces/SourceFormat';
import { ApiManagerService } from '../../services/api-manager.service';
import { tap } from 'rxjs/operators';

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
        this.apiManagerService.refreshSourceFormats.subscribe(
            newSourceFormats =>
                newSourceFormats.forEach((sourceFormat: SourceFormat) =>
                    this.sourceFormatButtons.push({
                        value: sourceFormat.sourceFormatId.toString(),
                        name: sourceFormat.sourceFormatName
                    })
                )
        );

        this.apiManagerService.refreshSources.subscribe(newSources => {
            libraryFilteringService.sources = newSources;
            this.searchField.searchControl.reset();
        });
    }

    ngOnInit(): void {}

    ngAfterViewInit(): void {
        this.searchField.searchControl.valueChanges.subscribe(
            (newData: string) =>
                (this.libraryFilteringService.sourcesFiltered =
                    newData === '' || newData === null
                        ? this.libraryFilteringService.sources
                        : this.libraryFilteringService.sources.filter(
                              (source: Source) =>
                                  !!source.sourceName
                                      .toLowerCase()
                                      .includes(newData.toLowerCase())
                          ))
        );
    }
}
