import { Component, OnInit } from '@angular/core';
import { ThemeService } from '../../theme.service';

@Component({
    selector: 'app-theme-picker',
    templateUrl: './theme-picker.component.html',
    styleUrls: ['./theme-picker.component.scss']
})
export class ThemePickerComponent implements OnInit {
    private currentTheme = '';

    constructor(private themeService: ThemeService) {
        this.currentTheme = themeService.getCookieTheme();
        themeService.themeChanged$.subscribe(theme => {
            this.currentTheme = theme;
        });
    }

    toggleTheme() {
        this.currentTheme =
            this.currentTheme === 'whvm-theme-Light'
                ? 'whvm-theme-Dark'
                : 'whvm-theme-Light';
        this.themeService.changeTheme(this.currentTheme);
    }

    ngOnInit() {
    }
}
