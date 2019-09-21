import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ApiManagerService } from '../../services/api-manager.service';
import { FormControl } from '@angular/forms';
import { combineLatest } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { IFilter } from '../../../interfaces/IFilter';

import moment from 'moment';
import { LibrarySourcesService } from '../library-sources.service';

@Component({
    selector: 'app-library-sources-sidebar',
    templateUrl: './library-sources-sidebar.component.html',
    styleUrls: ['./library-sources-sidebar.component.scss']
})
export class LibrarySourcesSidebarComponent implements OnInit {
    sourceSearchField = new FormControl();
    @Input() public sourceSearchFieldValue = '';

    @Input() public sourceDateStart: moment.Moment;
    sourceDatePickerStart = new FormControl(this.sourceDateStart);

    @Input() public sourceDateEnd: moment.Moment;
    sourceDatePickerEnd = new FormControl(this.sourceDateEnd);

    sourceFormatFilterButtons = new FormControl();
    @Input() public formatFilterButtonValue = '';

    private filterChangesInputs = combineLatest(
        this.sourceSearchField.valueChanges.pipe(
            startWith(''),
            map(x => ({
                comparer: 'match',
                comparisonField: 'sourceName',
                filtered: null,
                value: x
            }))
        ),
        this.sourceFormatFilterButtons.valueChanges.pipe(
            startWith(''),
            map(x => ({
                comparer: '===',
                comparisonField: 'sourceFormatId',
                filtered: null,
                value: x
            }))
        ),
        this.sourceDatePickerStart.valueChanges.pipe(
            startWith(null),
            map(x => ({
                comparer: '>=',
                comparisonField: ['sourceDateStart', 'sourceDateEnd'],
                filtered: null,
                value: x
            }))
        ),
        this.sourceDatePickerEnd.valueChanges.pipe(
            startWith(null),
            map(x => ({
                comparer: '<=',
                comparisonField: ['sourceDateStart', 'sourceDateEnd'],
                filtered: null,
                value: x
            }))
        )
    );

    @Output() public filterChanges = new EventEmitter<IFilter[]>();

    sourceFormatButtons;

    constructor(private apiManagerService: ApiManagerService, private librarySourcesService: LibrarySourcesService) {
        this.apiManagerService.getSourceFormats().subscribe(
            () =>
                (this.sourceFormatButtons = this.apiManagerService.sourceFormats.map(
                    newSourceFormat => ({
                        value: newSourceFormat.sourceFormatId.toString(),
                        name: newSourceFormat.sourceFormatName
                    })
                ))
        );

        this.filterChangesInputs.subscribe(x => this.librarySourcesService.activeSourceFilters$.next(x));
    }

    ngOnInit() {
    }
}
