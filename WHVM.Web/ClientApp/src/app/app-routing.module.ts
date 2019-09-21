import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LibraryComponent } from './library/library.component';
import { LibraryGuard } from './library/library.guard';
import { LibrarySourcesComponent } from './library/library-sources/library-sources.component';
import { LibrarySourcesSidebarComponent } from './library/library-sources-sidebar/library-sources-sidebar.component';
import { LibraryClipsSidebarComponent } from './library/library-clips-sidebar/library-clips-sidebar.component';
import { LibraryClipsComponent } from './library/library-clips/library-clips.component';

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
        component: LibraryComponent,
        canActivate: [LibraryGuard],
        children: [
            {
                path: '',
                pathMatch: 'full',
                redirectTo: 'sources'
            },
            {
                path: 'sources',
                children: [
                    {
                        path: '',
                        component: LibrarySourcesComponent,
                        outlet: 'primary'
                    },
                    {
                        path: '',
                        component: LibrarySourcesSidebarComponent,
                        outlet: 'sidebar'
                    }
                ]
            },
            {
                path: 'clips',
                children: [
                    {
                        path: '',
                        component: LibraryClipsComponent,
                        outlet: 'primary'
                    },
                    {
                        path: '',
                        component: LibraryClipsSidebarComponent,
                        outlet: 'sidebar'
                    }
                ]
            }
        ]
    }
];

@NgModule({
    declarations: [],
    imports: [
        RouterModule.forRoot(
            routes,
            { enableTracing: true } // <-- debugging purposes only
        )
    ],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
