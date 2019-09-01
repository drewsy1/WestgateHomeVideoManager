import { Component, Input } from '@angular/core';
import { IsHandsetService } from '../services/is-handset.service';
import { PageDataService } from '../services/page-data.service';

interface SiteSection {
    name: string;
    link: string;
}

@Component({
    selector: 'app-nav-base',
    templateUrl: './nav-base.component.html',
    styleUrls: ['./nav-base.component.scss']
})
export class NavBaseComponent {
    @Input() siteTitle: string;
    pageTitle: string;
    siteSections: SiteSection[] = [{ name: 'Library', link: 'library' }];

    private isHandset;

    constructor(
        private isHandsetService: IsHandsetService,
        private pageDataService: PageDataService
    ) {
        isHandsetService.isHandset$.subscribe(ih => (this.isHandset = ih));
        pageDataService.pageTitleChanged$.subscribe(
            newPageTitle => (this.pageTitle = newPageTitle)
        );
    }
}
