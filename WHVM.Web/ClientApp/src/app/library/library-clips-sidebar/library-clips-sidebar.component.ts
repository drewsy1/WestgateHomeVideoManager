import { Component, Input, OnInit } from '@angular/core';
import { ApiManagerService } from '../../services/api-manager.service';
import { combineLatest } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { LibraryClipsService } from '../library-clips.service';
import { FormControl } from '@angular/forms';

@Component({
    selector: 'app-library-clips-sidebar',
    templateUrl: './library-clips-sidebar.component.html',
    styleUrls: ['./library-clips-sidebar.component.scss']
})
export class LibraryClipsSidebarComponent implements OnInit {
    clipSearchField = new FormControl();
    @Input() public clipSearchFieldValue = '';

    private filterChangesInputs = combineLatest(
        this.clipSearchField.valueChanges.pipe(
            startWith(''),
            map(x => ({
                comparer: 'match',
                comparisonField: 'clipName',
                filtered: null,
                value: x
            }))
        )
    );

    constructor(private apiManagerService: ApiManagerService, private libraryClipsService: LibraryClipsService) {
        this.filterChangesInputs.subscribe(x => this.libraryClipsService.activeClipFilters$.next(x));
    }

    ngOnInit() {
    }

}
