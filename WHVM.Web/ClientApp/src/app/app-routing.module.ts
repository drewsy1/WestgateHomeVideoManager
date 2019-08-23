import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NavBaseComponent } from './nav-base/nav-base.component';

const routes: Routes = [
    {
        path: '',
        component: HomeComponent
    }
];

@NgModule({
    declarations: [],
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {}
