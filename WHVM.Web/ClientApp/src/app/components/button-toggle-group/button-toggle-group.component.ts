import { Component, Input, OnInit } from '@angular/core';

export interface ButtonToggle {
    value: string;
    name: string;
    checked?: boolean;
}

@Component({
    selector: 'app-button-toggle-group',
    templateUrl: './button-toggle-group.component.html',
    styleUrls: ['./button-toggle-group.component.scss']
})
export class ButtonToggleGroupComponent implements OnInit {
    @Input() name: string;
    @Input() buttons: ButtonToggle[];

    value = '';

    constructor() {}

    ngOnInit() {
    }
}
