import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { CookieManagerService } from './cookie-manager.service';

@Injectable({
    providedIn: 'root'
})
export class ThemeService {
    public defaultTheme = 'whvm-theme-Light';
    private currentTheme = new Subject<string>();

    themeChanged$ = this.currentTheme.asObservable();

    changeTheme(theme: string) {
        this.cookieManagerService.displayTheme = theme;
        this.currentTheme.next(theme);
    }

    getCookieTheme() {
        return this.cookieManagerService.displayTheme || this.defaultTheme;
    }

    constructor(private cookieManagerService: CookieManagerService) {
    }
}
