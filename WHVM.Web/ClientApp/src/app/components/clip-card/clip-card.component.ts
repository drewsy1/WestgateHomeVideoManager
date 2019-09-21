import { Component, Input, OnInit } from '@angular/core';
import { IClip } from '../../../interfaces/IClip';

@Component({
    selector: 'app-clip-card',
    templateUrl: './clip-card.component.html',
    styleUrls: ['./clip-card.component.scss']
})
export class ClipCardComponent implements OnInit {
    @Input() clip: IClip;

    constructor() {
    }

    ngOnInit() {
    }
}
