import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Source } from '../../interfaces/Source';
import { SourceFormat } from '../../interfaces/SourceFormat';
import { tap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class ApiManagerService {
    apiURL = 'http://localhost:5000/api/';

    sources: Source[] = [];
    sourceFormats: SourceFormat[] = [];

    refreshSources = this.getSources().pipe(
        tap(newSources => (this.sources = newSources))
    );

    refreshSourceFormats = this.getSourceFormats().pipe(
        tap(newSourceFormats => (this.sourceFormats = newSourceFormats))
    );

    constructor(private http: HttpClient) {
    }

    getSources(): Observable<Source[]> {
        const sourcesURL = `${this.apiURL}sources/`;
        return this.http.get(sourcesURL) as Observable<Source[]>;
    }

    getSourceFormats(): Observable<SourceFormat[]> {
        const formatsURL = `${this.apiURL}sourceformats/`;
        return this.http.get(formatsURL) as Observable<SourceFormat[]>;
    }
}
