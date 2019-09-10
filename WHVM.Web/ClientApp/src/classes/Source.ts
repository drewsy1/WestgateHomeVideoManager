import { ISourceFormat } from '../interfaces/ISourceFormat';

export class Source {
    files: [];
    sourceDateCreated: Date;
    sourceDateEnd: string;
    sourceDateImported: string;
    sourceDateStart: string;
    sourceFilePath: string;
    sourceFormatId: number;
    sourceId: number;
    sourceName: string;
    sourceFormat: ISourceFormat;

    constructor() {
    }
}
