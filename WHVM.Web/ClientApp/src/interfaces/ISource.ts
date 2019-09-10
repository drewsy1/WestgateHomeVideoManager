import { ISourceFormat } from './ISourceFormat';
import { IPerson } from './IPerson';
import { ICollection } from './ICollection';

export interface ISource {
    sourceId: number;
    sourceName: string;
    sourceDateCreated: Date;
    sourceDateImported: Date;
    sourceFormatId: number;
    sourceFormat: ISourceFormat;
    sourceFilePath: string;
    files: [];
    sourceDateStart: Date;
    sourceDateEnd: Date;
    sourcePersons: IPerson[];
    sourceCollections: ICollection[];

    [key: string]: any;
}
