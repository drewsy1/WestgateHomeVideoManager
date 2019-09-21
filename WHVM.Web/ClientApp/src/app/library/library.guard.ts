import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { mapTo } from 'rxjs/operators';
import { ApiManagerService } from '../services/api-manager.service';
import { LibrarySourcesService } from './library-sources.service';
import { LibraryClipsService } from './library-clips.service';

@Injectable({
    providedIn: 'root'
})
export class LibraryGuard implements CanActivate {
    constructor(
        private librarySourcesService: LibrarySourcesService,
        private libraryClipsService: LibraryClipsService,
        private apiManagerService: ApiManagerService
    ) {
    }

    canActivate() {
        return (
            this.apiManagerService.getSources().pipe(mapTo(true))
            && this.librarySourcesService.getLibrarySourcesData().pipe(mapTo(true))
            && this.libraryClipsService.getLibraryClipsData().pipe(mapTo(true))
        );
    }
}
