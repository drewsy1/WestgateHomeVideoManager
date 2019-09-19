import { ISource } from './ISource';

export interface IFilter {
    comparer: string;
    comparisonField: string | string[];
    filtered: ISource[];
    value?: any;
}
