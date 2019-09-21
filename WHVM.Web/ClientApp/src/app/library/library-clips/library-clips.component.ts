import { Component, OnInit } from '@angular/core';
import { ApiManagerService } from '../../services/api-manager.service';
import { first } from 'rxjs/operators';
import { LibraryClipsService } from '../library-clips.service';

@Component({
    selector: 'app-library-clips',
    templateUrl: './library-clips.component.html',
    styleUrls: ['./library-clips.component.scss']
})
export class LibraryClipsComponent implements OnInit {
    loadedContent = this.libraryClipsService.loadedContent;

    constructor(
        private libraryClipsService: LibraryClipsService,
        private apiManagerService: ApiManagerService
    ) {
        if (!this.apiManagerService.clips) {
            this.libraryClipsService.getLibraryClipsData();
        }

        this.apiManagerService.getClipsSubject
            .pipe(first())
            .subscribe(() => this.libraryClipsService.loadedContent$.next());
    }

    ngOnInit() {
    }

}
