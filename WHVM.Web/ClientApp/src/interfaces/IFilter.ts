import { ISource } from './ISource';
import { IClip } from './IClip';

export interface IFilter {
    comparer: string;
    comparisonField: string | string[];
    filtered: ISource[] | IClip[];
    value?: any;
}
