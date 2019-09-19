import { AfterViewInit, Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { PageDataService } from '../services/page-data.service';
import { LibraryService } from './library.service';
import { MatDrawerContainer, MatTabGroup } from '@angular/material';

@Component({
    selector: 'app-library',
    templateUrl: './library.component.html',
    styleUrls: ['./library.component.scss']
})
export class LibraryComponent implements OnInit, OnDestroy, AfterViewInit {
    @ViewChild(MatDrawerContainer, { static: false })
    drawerContainer: MatDrawerContainer;

    @ViewChild(MatTabGroup, { static: false })
    matTabGroup: MatTabGroup;

    tabs = ['Sources', 'Clips'];
    @Input() tabCurrent = 0;

    constructor(
        private pageDataService: PageDataService,
        private libraryFilteringService: LibraryService
    ) {
        this.libraryFilteringService.loadedContent$
            .subscribe(() => this.drawerContainer.updateContentMargins());

        this.pageDataService.setPageTitle(this.tabs[this.tabCurrent]);
    }

    ngOnInit() {
    }

    ngAfterViewInit(): void {
        this.matTabGroup.selectedIndexChange.subscribe(index =>
            this.pageDataService.setPageTitle(this.tabs[index])
        );
    }

    ngOnDestroy(): void {
    }
}
