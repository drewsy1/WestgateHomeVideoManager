import { Injectable } from '@angular/core';
import { combineLatest, Subject } from 'rxjs';
import { Title } from '@angular/platform-browser';
import { startWith } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class PageDataService {
    public siteTitle = '';
    public siteTitle$ = new Subject<string>();
    public pageTitle = '';
    public pageTitle$ = new Subject<string>();

    setPageTitle(newPageTitle: string) {
        this.pageTitle$.next(newPageTitle);
    }

    setSiteTitle(newSiteTitle: string) {
        this.siteTitle$.next(newSiteTitle);
    }

    changeSiteAndPageTitle(siteTitle: string, pageTitle: string) {
        this.titleService.setTitle(`${siteTitle} - ${pageTitle}`);
    }

    constructor(private titleService: Title) {
        combineLatest(
            this.siteTitle$.pipe(startWith(this.siteTitle)),
            this.pageTitle$.pipe(startWith(this.pageTitle))
        ).subscribe(x =>
            // console.log(x)
            this.changeSiteAndPageTitle(x[0], x[1])
        );

        this.siteTitle$.subscribe(
            newSiteTitle => (this.siteTitle = newSiteTitle)
        );
        this.pageTitle$.subscribe(
            newPageTitle => (this.pageTitle = newPageTitle)
        );
    }
}
