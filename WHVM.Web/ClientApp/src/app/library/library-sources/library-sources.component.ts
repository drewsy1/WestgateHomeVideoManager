import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { ApiManagerService } from '../../services/api-manager.service';
import { LibrarySourcesService } from '../library-sources.service';

@Component({
    selector: 'app-library-sources',
    templateUrl: './library-sources.component.html',
    styleUrls: ['./library-sources.component.scss']
})
export class LibrarySourcesComponent implements OnInit {
    loadedContent$ = this.librarySourcesService.loadedContent$;

    constructor(
        private librarySourcesService: LibrarySourcesService,
        private apiManagerService: ApiManagerService
    ) {
        if (!this.apiManagerService.sources) {
            this.librarySourcesService.getLibrarySourcesData();
        }

        this.apiManagerService.getSourcesSubject
            .pipe(first())
            .subscribe(() => this.librarySourcesService.loadedContent$.next());
    }

    ngOnInit() {
    }

}
