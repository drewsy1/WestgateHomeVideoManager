import { Component, HostBinding } from '@angular/core';
import { BreakpointObserver } from '@angular/cdk/layout';
import { OverlayContainer } from '@angular/cdk/overlay';
import { ThemeService } from './theme.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
    providers: [ThemeService]
})
export class AppComponent {
    siteTitle = 'Westgate Home Video Manager';

    @HostBinding('class') componentCssClass;

    constructor(
        private breakpointObserver: BreakpointObserver,
        private overlayContainer: OverlayContainer,
        private themeService: ThemeService
    ) {
        themeService.changeTheme(themeService.getCookieTheme());
        themeService.themeChanged$.subscribe(theme => {
            this.componentCssClass = `${theme} mat-app-background`;
        });
    }
}
