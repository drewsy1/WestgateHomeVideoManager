import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CookieService } from 'ngx-cookie-service';

import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatTooltipModule } from '@angular/material/tooltip';
import { FlexLayoutModule } from '@angular/flex-layout';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { NavBaseComponent } from './nav-base/nav-base.component';
import { HomeComponent } from './home/home.component';
import { ThemePickerComponent } from './nav-base/theme-picker/theme-picker.component';
import { ThemePickerMobileComponent } from './nav-base/theme-picker/theme-picker-mobile/theme-picker-mobile.component';
import { LibraryComponent } from './library/library.component';
import { PageHeaderComponent } from './nav-base/page-header/page-header.component';
import { CookieManagerService } from './cookie-manager.service';
import { IsHandsetService } from './is-handset.service';
import { ThemeService } from './theme.service';
import { PageDataService} from './page-data.service';


@NgModule({
    declarations: [
        AppComponent,
        NavBaseComponent,
        HomeComponent,
        ThemePickerComponent,
        ThemePickerMobileComponent,
        LibraryComponent,
        PageHeaderComponent
    ],
    providers: [
        CookieService,
        CookieManagerService,
        IsHandsetService,
        PageDataService,
        ThemeService
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        LayoutModule,
        MatToolbarModule,
        MatButtonModule,
        MatSidenavModule,
        MatIconModule,
        MatListModule,
        AppRoutingModule,
        MatTooltipModule,
        FlexLayoutModule
    ],
    bootstrap: [AppComponent]
})
export class AppModule {}
