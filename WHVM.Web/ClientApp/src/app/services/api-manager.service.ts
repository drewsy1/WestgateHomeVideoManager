import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Source } from '../../interfaces/Source';

@Injectable({
    providedIn: 'root'
})
export class ApiManagerService {
    apiURL = 'http://localhost:5000/api/';

    constructor(private http: HttpClient) {
    }

    getSources(): Observable<Source[]> {
        const heroesURL = `${this.apiURL}sources/`;
        return this.http.get(heroesURL) as Observable<Source[]>;
    }
}
