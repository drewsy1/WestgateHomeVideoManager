import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { PageDataService } from '../services/page-data.service';
import { LibraryService } from './library.service';
import { MatDrawerContainer } from '@angular/material';
import { ApiManagerService } from '../services/api-manager.service';
import { first } from 'rxjs/operators';

@Component({
    selector: 'app-library',
    templateUrl: './library.component.html',
    styleUrls: ['./library.component.scss']
})
export class LibraryComponent implements OnInit, OnDestroy, AfterViewInit {
    @ViewChild(MatDrawerContainer, { static: false })
    drawerContainer: MatDrawerContainer;

    constructor(
        private pageDataService: PageDataService,
        private libraryFilteringService: LibraryService,
        private apiManagerService: ApiManagerService
    ) {
        if (!this.apiManagerService.sources) {
            this.libraryFilteringService.getLibraryData();
        }

        this.apiManagerService.getSourcesSubject
            .pipe(first())
            .subscribe(() => this.drawerContainer.updateContentMargins());
    }

    ngOnInit() {
        this.pageDataService.changePageTitle('Library');
    }

    ngAfterViewInit(): void {
    }

    ngOnDestroy(): void {
        this.pageDataService.changePageTitle(undefined);
    }
}
