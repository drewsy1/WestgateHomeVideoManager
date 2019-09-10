import { Component, Input, OnInit } from '@angular/core';
import { ISource } from '../../../interfaces/ISource';

@Component({
    selector: 'app-source-card',
    templateUrl: './source-card.component.html',
    styleUrls: ['./source-card.component.scss']
})
export class SourceCardComponent implements OnInit {
    @Input() source: ISource;

    chipChange(arg: any) {
        console.log(arg);
    }

    constructor() {
    }

    ngOnInit() {
    }
}
