import { Injectable } from '@angular/core';
import { Source } from '../../interfaces/Source';
import { ApiManagerService } from '../services/api-manager.service';
import { Subject } from 'rxjs';
import { SourceFormat } from '../../interfaces/SourceFormat';
import { tap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class LibraryFilteringService {
    constructor(public apiManagerService: ApiManagerService) {
    }
    sources: Source[] = [];
    sourcesFiltered: Source[] = [];
}
