import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

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
    @Output() valueChanges: EventEmitter<string> = new EventEmitter<string>();

    value = '';

    constructor() {
    }

    ngOnInit() {
    }
}
