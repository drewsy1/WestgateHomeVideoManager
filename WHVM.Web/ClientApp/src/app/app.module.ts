// Angular
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';
import { AppRoutingModule } from './app-routing.module';
import { FlexLayoutModule } from '@angular/flex-layout';
// Angular Material
import {
    MatChipsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatProgressBarModule,
    MatProgressSpinnerModule
} from '@angular/material';
import { LayoutModule } from '@angular/cdk/layout';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatTooltipModule } from '@angular/material/tooltip';
// Components
import { AppComponent } from './app.component';
import { NavBaseComponent } from './nav-base/nav-base.component';
import { HomeComponent } from './home/home.component';
import { ThemePickerComponent } from './nav-base/theme-picker/theme-picker.component';
import { ThemePickerMobileComponent } from './nav-base/theme-picker/theme-picker-mobile/theme-picker-mobile.component';
import { LibraryComponent } from './library/library.component';
import { PageHeaderComponent } from './nav-base/page-header/page-header.component';
import { SearchFieldComponent } from './components/search-field/search-field.component';
import { LibrarySidebarComponent } from './library/library-sidebar/library-sidebar.component';
import { ButtonToggleGroupComponent } from './components/button-toggle-group/button-toggle-group.component';
import { SourceCardComponent } from './components/source-card/source-card.component';
import { DatePickerComponent } from './components/date-picker/date-picker.component';
import { DateRangePickerComponent } from './components/date-range-picker/date-range-picker.component';
import { FilterButtonGroupComponent } from './library/library-sidebar/filter-button-group/filter-button-group.component';
// Services
import { CookieManagerService } from './services/cookie-manager.service';
import { IsHandsetService } from './services/is-handset.service';
import { ThemeService } from './services/theme.service';
import { PageDataService } from './services/page-data.service';
import { ApiManagerService } from './services/api-manager.service';
import { LibraryService } from './library/library.service';


@NgModule({
    declarations: [
        AppComponent,
        NavBaseComponent,
        HomeComponent,
        ThemePickerComponent,
        ThemePickerMobileComponent,
        LibraryComponent,
        PageHeaderComponent,
        LibrarySidebarComponent,
        SearchFieldComponent,
        ButtonToggleGroupComponent,
        SourceCardComponent,
        DatePickerComponent,
        DateRangePickerComponent,
        FilterButtonGroupComponent
    ],
    providers: [
        CookieService,
        CookieManagerService,
        IsHandsetService,
        PageDataService,
        ThemeService,
        ApiManagerService,
        LibraryService
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        LayoutModule,
        MatNativeDateModule,
        MatToolbarModule,
        MatButtonModule,
        MatSidenavModule,
        MatIconModule,
        MatListModule,
        MatTooltipModule,
        FlexLayoutModule,
        MatCardModule,
        MatTableModule,
        MatButtonToggleModule,
        MatFormFieldModule,
        MatInputModule,
        ReactiveFormsModule,
        FormsModule,
        MatChipsModule,
        MatProgressSpinnerModule,
        MatProgressBarModule,
        MatDatepickerModule
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
