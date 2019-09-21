import * as moment from 'moment';
import { IPerson } from './IPerson';
import { ICollection } from './ICollection';

export interface IClip {
    clipId: number;
    sourceSegment?: number;
    sourceId?: number;
    clipNumber?: number;
    clipTimeStart?: moment.Moment;
    clipTimeEnd?: moment.Moment;
    clipVidTimeStart?: moment.Duration;
    clipVidTimeEnd?: moment.Duration;
    clipVidTimeLength?: moment.Duration;
    clipReviewerId?: number;
    clipCameraOperatorId?: number;
    clipName?: string;
    clipReviewer?: IPerson;
    clipCameraOperator?: IPerson;
    persons?: IPerson[];
    collections?: ICollection[];
}
