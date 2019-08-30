import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PageDataService {
    private currentPageTitle = new Subject<string>();

    pageTitleChanged$ = this.currentPageTitle.asObservable();

    changePageTitle(pageTitle: string) {
        this.currentPageTitle.next(pageTitle);
    }

    constructor() { }
}
