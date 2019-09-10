import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { from, Observable, Subject } from 'rxjs';
import { ISource } from '../../interfaces/ISource';
import { ISourceFormat } from '../../interfaces/ISourceFormat';
import { distinctUntilKeyChanged, mergeMap, tap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class ApiManagerService {
    apiURL = 'api/';

    sources: ISource[] = [];
    sourceFormats: ISourceFormat[] = [];

    getSourcesSubject = new Subject<ISource>();
    getSourceFormatsSubject = new Subject<ISourceFormat>();

    constructor(private http: HttpClient) {
    }

    conditionalArrayAdd(targetArray: any[], newItem: any, id: string) {
        return targetArray.concat([newItem]).filter((value, index, array) => {
            const filteredArray = array.filter(x => value[id] === x[id]);
            return filteredArray
                ? value === filteredArray[filteredArray.length - 1]
                : false;
        });
    }

    getSources(): Observable<ISource> {
        const sourcesURL = `${this.apiURL}sources/`;
        return this.http.get<ISource[]>(sourcesURL).pipe(
            mergeMap((sourcesArray: ISource[]) =>
                from(sourcesArray).pipe(
                    distinctUntilKeyChanged('sourceId'),
                    tap(
                        newSource =>
                            (this.sources = this.conditionalArrayAdd(
                                this.sources,
                                newSource,
                                'sourceId'
                            ))
                    ),
                    tap(newSource => this.getSourcesSubject.next(newSource))
                )
            )
        );
    }

    getSourceFormats(): Observable<ISourceFormat> {
        const formatsURL = `${this.apiURL}sourceformats/`;
        return this.http.get<ISourceFormat[]>(formatsURL).pipe(
            mergeMap((sourceFormatsArray: ISourceFormat[]) =>
                from(sourceFormatsArray).pipe(
                    distinctUntilKeyChanged('sourceFormatId'),
                    tap(
                        newSourceFormat =>
                            (this.sourceFormats = this.conditionalArrayAdd(
                                this.sourceFormats,
                                newSourceFormat,
                                'sourceFormatId'
                            ))
                    ),
                    tap(newSourceFormat =>
                        this.getSourceFormatsSubject.next(newSourceFormat)
                    )
                )
            )
        );
    }

    // getSourceFormatFromId(sourceFormatId) {
    //     return this.sourceFormats.find(
    //         format => format.sourceFormatId === sourceFormatId
    //     );
    // }
}
