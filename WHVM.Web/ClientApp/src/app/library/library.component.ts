import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { PageDataService } from '../services/page-data.service';
import { LibraryFilteringService } from './library-filtering.service';
import { MatDrawerContainer } from '@angular/material';
import { ApiManagerService } from '../services/api-manager.service';

@Component({
    selector: 'app-library',
    templateUrl: './library.component.html',
    styleUrls: ['./library.component.scss']
})
export class LibraryComponent implements OnInit, OnDestroy {
    @ViewChild(MatDrawerContainer, { static: false })
    drawerContainer: MatDrawerContainer;

    constructor(
        private pageDataService: PageDataService,
        private libraryFilteringService: LibraryFilteringService,
        private apiManagerService: ApiManagerService
    ) {
        this.apiManagerService.getSourcesSubject.subscribe(() =>
            this.drawerContainer.updateContentMargins()
        );
        this.apiManagerService.getSources().subscribe();
        this.apiManagerService.getSourceFormats().subscribe();
    }

    ngOnInit() {
        this.pageDataService.changePageTitle('Library');
    }

    ngOnDestroy(): void {
        this.pageDataService.changePageTitle(undefined);
    }
}
