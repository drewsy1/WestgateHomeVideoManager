import { Component, OnDestroy, OnInit } from '@angular/core';
import {PageDataService} from '../page-data.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, OnDestroy {
    constructor(private pageDataService: PageDataService) {}

    ngOnInit() {
        this.pageDataService.changePageTitle('Home');
    }

    ngOnDestroy(): void {
        this.pageDataService.changePageTitle(undefined);
    }
}
