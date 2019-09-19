import { Component, OnInit } from '@angular/core';
import { LibraryService } from '../library.service';
import { first } from 'rxjs/operators';
import { ApiManagerService } from '../../services/api-manager.service';

@Component({
    selector: 'app-library-sources',
    templateUrl: './library-sources.component.html',
    styleUrls: ['./library-sources.component.scss']
})
export class LibrarySourcesComponent implements OnInit {


    constructor(
        private libraryFilteringService: LibraryService,
        private apiManagerService: ApiManagerService
    ) {
        if (!this.apiManagerService.sources) {
            this.libraryFilteringService.getLibraryData();
        }

        this.apiManagerService.getSourcesSubject
            .pipe(first())
            .subscribe(() => this.libraryFilteringService.loadedContent$.next());
    }

    ngOnInit() {
    }

}
