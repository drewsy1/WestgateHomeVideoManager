import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { CookieManagerService } from './cookie-manager.service';

@Injectable({
    providedIn: 'root'
})
export class ThemeService {
    private currentTheme = new Subject<string>();

    themeChanged$ = this.currentTheme.asObservable();

    changeTheme(theme: string) {
        this.cookieManagerService.displayTheme = theme;
        this.currentTheme.next(theme);
    }

    setFromCookieTheme() {
        this.changeTheme(this.cookieManagerService.displayTheme || 'whvm-theme-Light');
    }

    constructor(private cookieManagerService: CookieManagerService) {
    }
}
