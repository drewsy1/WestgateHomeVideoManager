import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { ISource } from '../../interfaces/ISource';
import { ISourceFormat } from '../../interfaces/ISourceFormat';
import { tap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class ApiManagerService {
    apiURL = 'api/';

    sources: ISource[] = [];
    sourceFormats: ISourceFormat[] = [];

    getSourcesSubject = new Subject<ISource[]>();
    getSourceFormatsSubject = new Subject<ISourceFormat[]>();

    constructor(private http: HttpClient) {
    }

    getSources(): Observable<ISource[]> {
        const sourcesURL = `${this.apiURL}sources/`;
        return this.http.get<ISource[]>(sourcesURL).pipe(
            tap(newSources => (this.sources = newSources)),
            tap(newSources => this.getSourcesSubject.next(newSources))
        );
    }

    getSourceFormats(): Observable<ISourceFormat[]> {
        const formatsURL = `${this.apiURL}sourceformats/`;
        return this.http.get<ISourceFormat[]>(formatsURL).pipe(
            tap(newSourceFormats => (this.sourceFormats = newSourceFormats)),
            tap(newSourceFormats =>
                this.getSourceFormatsSubject.next(newSourceFormats)
            )
        );
    }
}
