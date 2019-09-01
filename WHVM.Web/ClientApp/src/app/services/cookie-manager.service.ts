import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
    providedIn: 'root'
})
export class CookieManagerService {
    get displayTheme() {
        return this.cookieService.check('displayTheme')
            ? this.cookieService.get('displayTheme')
            : null;
    }

    set displayTheme(newTheme: string) {
        this.cookieService.set('displayTheme', newTheme);
    }

    constructor(private cookieService: CookieService) {
    }
}
