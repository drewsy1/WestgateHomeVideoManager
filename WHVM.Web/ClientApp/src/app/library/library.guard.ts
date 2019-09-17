import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { LibraryService } from './library.service';
import { mapTo } from 'rxjs/operators';
import { ApiManagerService } from '../services/api-manager.service';

@Injectable({
    providedIn: 'root'
})
export class LibraryGuard implements CanActivate {
    constructor(
        private libraryService: LibraryService,
        private apiManagerService: ApiManagerService
    ) {
    }

    canActivate() {
        return (
            this.apiManagerService.getSources().pipe(mapTo(true))
            && this.libraryService.getLibraryData().pipe(mapTo(true))
        );
    }
}
