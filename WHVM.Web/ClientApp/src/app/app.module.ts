import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatTooltipModule } from '@angular/material/tooltip';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { NavBaseComponent } from './nav-base/nav-base.component';
import { HomeComponent } from './home/home.component';
import { ThemePickerComponent } from './nav-base/theme-picker/theme-picker.component';
import { ThemePickerMobileComponent } from './nav-base/theme-picker/theme-picker-mobile/theme-picker-mobile.component';

@NgModule({
    declarations: [
        AppComponent,
        NavBaseComponent,
        HomeComponent,
        ThemePickerComponent,
        ThemePickerMobileComponent
    ],
    providers: [],
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
        MatTooltipModule
    ],
    bootstrap: [AppComponent]
})
export class AppModule {}
