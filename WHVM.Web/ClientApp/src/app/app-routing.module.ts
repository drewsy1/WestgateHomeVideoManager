import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LibraryComponent } from './library/library.component';

const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'home'
    },
    {
        path: 'home',
        component: HomeComponent
    },
    {
        path: 'library',
        component: LibraryComponent
    }
];

@NgModule({
    declarations: [],
    imports: [RouterModule.forRoot(
        routes,
        { enableTracing: true } // <-- debugging purposes only
    )],
    exports: [RouterModule]
})
export class AppRoutingModule {}
