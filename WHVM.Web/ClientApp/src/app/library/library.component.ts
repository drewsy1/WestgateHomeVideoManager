import { Component, OnInit, OnDestroy } from '@angular/core';
import { PageDataService } from '../page-data.service';

@Component({
    selector: 'app-library',
    templateUrl: './library.component.html',
    styleUrls: ['./library.component.scss']
})
export class LibraryComponent implements OnInit, OnDestroy {
    constructor(private pageDataService: PageDataService) {}

    ngOnInit() {
        this.pageDataService.changePageTitle('Library');
    }

    ngOnDestroy(): void {
        this.pageDataService.changePageTitle(undefined);
    }
}
