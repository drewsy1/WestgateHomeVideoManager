import { Component, OnInit } from '@angular/core';
import { ThemeService } from '../theme.service';

@Component({
    selector: 'app-theme-picker',
    templateUrl: './theme-picker.component.html',
    styleUrls: ['./theme-picker.component.scss']
})
export class ThemePickerComponent implements OnInit {
    private _currentTheme: string = 'whvm-theme-Light';

    constructor(private themeService: ThemeService) {
        themeService.themeChanged$.subscribe(theme => {
            this._currentTheme = theme;
        });
    }

    toggleTheme() {
        this._currentTheme =
            this._currentTheme == 'whvm-theme-Light'
                ? 'whvm-theme-Dark'
                : 'whvm-theme-Light';
        this.changeTheme(this._currentTheme);
    }

    changeTheme(theme: string) {
        this.themeService.changeTheme(theme);
    }

    ngOnInit() {
    }
}
