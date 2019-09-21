import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { ISource } from '../../interfaces/ISource';
import { ISourceFormat } from '../../interfaces/ISourceFormat';
import { map, tap } from 'rxjs/operators';
import moment from 'moment';
import { IClip } from '../../interfaces/IClip';

@Injectable({
    providedIn: 'root'
})
export class ApiManagerService {
    apiURL = 'api/';

    sources: ISource[] = [];
    sourceFormats: ISourceFormat[] = [];
    clips: IClip[] = [];

    getSourcesSubject = new Subject<ISource[]>();
    getSourceFormatsSubject = new Subject<ISourceFormat[]>();
    getClipsSubject = new Subject<IClip[]>();

    constructor(private http: HttpClient) {
    }

    getSources(): Observable<ISource[]> {
        const sourcesURL = `${this.apiURL}sources/`;
        return this.http.get<ISource[]>(sourcesURL).pipe(
            map(sources =>
                sources.map(source => {
                    [
                        'sourceDateStart',
                        'sourceDateEnd',
                        'sourceDateCreated',
                        'sourceDateImported'
                    ].forEach(
                        dateField =>
                            (source[dateField] = moment(
                                source[dateField]
                            ).isValid()
                                ? moment(source[dateField])
                                : null)
                    );

                    return source;
                })
            ),
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

    getClips(): Observable<IClip[]> {
        const clipsURL = `${this.apiURL}clips/`;
        return this.http.get<IClip[]>(clipsURL).pipe(
            map(clips =>
                clips.map(clip => {
                    [
                        'clipTimeStart',
                        'clipTimeEnd'
                    ].forEach(
                        dateField =>
                            (clip[dateField] = moment(
                                clip[dateField]
                            ).isValid()
                                ? moment(clip[dateField])
                                : null)
                    );

                    [
                        'clipVidTimeStart',
                        'clipVidTimeEnd',
                        'clipVidTimeLength'
                    ].forEach(
                        dateField =>
                            (clip[dateField] = moment.duration(
                                clip[dateField]
                            ).isValid()
                                ? moment.duration(clip[dateField])
                                : null)
                    );

                    return clip;
                })
            ),
            tap(newClips => (this.clips = newClips)),
            tap(newClips => this.getClipsSubject.next(newClips))
        );
    }
}
