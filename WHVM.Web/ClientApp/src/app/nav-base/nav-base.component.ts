import { Component, Input } from '@angular/core';
import { IsHandsetService } from '../is-handset.service';

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

    siteSections: SiteSection[] = [{ name: 'Library', link: 'library' }];

    private isHandset;

    constructor(private isHandsetService: IsHandsetService) {
        isHandsetService.isHandset$.subscribe(ih => (this.isHandset = ih));
    }
}
