import { Component, OnInit, HostBinding } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, share } from 'rxjs/operators';
import { OverlayContainer } from '@angular/cdk/overlay';
import { ThemeService } from './theme.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
    providers: [ThemeService]
})
export class AppComponent implements OnInit {
    siteTitle = 'Westgate Home Video Manager';

    @HostBinding('class') componentCssClass;

    onSetTheme(theme) {
        this.overlayContainer.getContainerElement().classList.add(theme);
        this.componentCssClass = `${theme} mat-app-background`;
    }

    isHandset$: Observable<boolean> = this.breakpointObserver
        .observe(Breakpoints.Handset)
        .pipe(
            map(result => result.matches),
            share()
        );

    constructor(
        private breakpointObserver: BreakpointObserver,
        private overlayContainer: OverlayContainer,
        private themeService: ThemeService
    ) {
        themeService.themeChanged$.subscribe(theme => {
            this.onSetTheme(theme);
        });
    }

    ngOnInit(): void {
        this.componentCssClass = 'mat-app-background';
    }
}
