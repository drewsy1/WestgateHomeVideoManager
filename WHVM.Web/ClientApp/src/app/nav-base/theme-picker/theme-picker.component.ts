import { Component, OnInit } from '@angular/core';
import { ThemeService } from '../../services/theme.service';

@Component({
    selector: 'app-theme-picker',
    template: `
        <button
                class="mat-icon-button"
                (click)="toggleTheme()"
                aria-haspopup="true"
                mat-icon-button
                matTooltip="Toggle dark theme"
                tabindex="-1"
                style="touch-action: none; user-select: none"
        >
            <mat-icon
                    class="mat-icon material-icons mat-icon-no-color"
                    role="img"
                    aria-hidden="true"
            >format_color_fill
            </mat-icon
            >
        </button>
    `,
    styles: [':host {padding: 0 16px}']
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
