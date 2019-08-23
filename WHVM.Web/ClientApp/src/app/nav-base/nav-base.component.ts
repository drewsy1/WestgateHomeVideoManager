import { Component, Input } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

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

    siteSections: SiteSection[] = [{ name: 'Sources', link: 'Sources' }];

    isHandset$: Observable<boolean> = this.breakpointObserver
        .observe(Breakpoints.Handset)
        .pipe(map(result => result.matches));

    constructor(private breakpointObserver: BreakpointObserver) {}
}
