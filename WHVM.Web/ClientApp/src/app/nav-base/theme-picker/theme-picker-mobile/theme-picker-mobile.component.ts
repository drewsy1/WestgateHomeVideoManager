import { Component, OnInit } from '@angular/core';
import { ThemePickerComponent } from '../theme-picker.component';
import { ThemeService } from '../../../services/theme.service';

@Component({
    selector: 'app-theme-picker-mobile',
    template: `
        <a
                mat-list-item
                href="#"
                (click)="toggleTheme()"
                aria-haspopup="true"
                matTooltip="Switch to {{ currentTheme.match('Dark') ? 'Light' : 'Dark'}} theme"
        >

            <mat-icon
                    matListIcon
                    class="mat-icon material-icons mat-icon-no-color"
                    role="img"
                    aria-hidden="true"
            >format_color_fill
            </mat-icon
            >
            <p>Switch Theme</p>
        </a>
    `
})
export class ThemePickerMobileComponent extends ThemePickerComponent implements OnInit {

    constructor(themeService: ThemeService) {
        super(themeService);
    }

    ngOnInit() {
    }

}
