import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { PageDataService } from '../services/page-data.service';
import { LibraryService } from './library.service';
import { MatDrawerContainer, MatTabGroup } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-library',
    templateUrl: './library.component.html',
    styleUrls: ['./library.component.scss']
})
export class LibraryComponent implements OnInit, OnDestroy, AfterViewInit {
    LibraryServiceClass = LibraryService;
    urlSegments = this.router.getCurrentNavigation().finalUrl.root.children.primary.segments;
    routeString = this.urlSegments[this.urlSegments.length - 1].toString();

    @ViewChild(MatDrawerContainer, { static: false })
    drawerContainer: MatDrawerContainer;

    @ViewChild(MatTabGroup, { static: false })
    matTabGroup: MatTabGroup;

    tabs = ['Sources', 'Clips'];
    tabCurrent = this.tabs.map(tab => tab.toLowerCase()).indexOf(this.routeString);


    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private pageDataService: PageDataService,
        private libraryFilteringService: LibraryService
    ) {
        this.libraryFilteringService.loadedContent$.subscribe(() =>
            this.drawerContainer.updateContentMargins()
        );

        this.pageDataService.setPageTitle(this.tabs[this.tabCurrent]);
    }

    ngOnInit() {
    }

    ngAfterViewInit(): void {
        this.matTabGroup.selectedIndexChange.subscribe(index => {
            this.pageDataService.setPageTitle(this.tabs[index]);
            this.router.navigate([this.tabs[index].toLowerCase()], {
                relativeTo: this.route
            }).finally();
        });
    }

    ngOnDestroy(): void {
    }
}
