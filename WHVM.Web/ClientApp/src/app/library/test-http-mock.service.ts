import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Source } from '../../interfaces/Source';


@Injectable()
export class HeroSearchService {
    apiURL = 'http://localhost:5000/api/';

    constructor(private http: HttpClient) {
    }

    getHeroes(): Observable<Source[]> {
        const heroesURL = `${this.apiURL}sources/`;
        return this.http.get(heroesURL) as Observable<Source[]>;
    }
}
