import { ISourceFormat } from './ISourceFormat';
import { IPerson } from './IPerson';
import { ICollection } from './ICollection';
import moment from 'moment';

export interface ISource {
    sourceId: number;
    sourceName: string;
    sourceDateCreated: moment.Moment;
    sourceDateImported: moment.Moment;
    sourceFormatId: number;
    sourceFormat: ISourceFormat;
    sourceFilePath: string;
    files: [];
    sourceDateStart: moment.Moment;
    sourceDateEnd: moment.Moment;
    sourcePersons: IPerson[];
    sourceCollections: ICollection[];

    [key: string]: any;
}
