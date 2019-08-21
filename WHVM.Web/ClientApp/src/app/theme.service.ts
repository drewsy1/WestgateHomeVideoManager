import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ThemeService {
    private currentTheme = new Subject<string>();

    themeChanged$ = this.currentTheme.asObservable();

    changeTheme(theme: string) {
        this.currentTheme.next(theme);
    }
}
