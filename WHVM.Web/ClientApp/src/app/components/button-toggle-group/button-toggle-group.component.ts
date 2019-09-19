import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

export interface ButtonToggle {
    value: string;
    name: string;
    checked?: boolean;
}

@Component({
    selector: 'app-button-toggle-group',
    template: `
        <mat-button-toggle-group
                name="{{ name }}"
                [(ngModel)]="value"
                (change)="valueChanges.emit(value)"
        >
            <mat-button-toggle
                    *ngFor="let button of buttons"
                    [value]="button.value"
                    [checked]="button.checked"
            >{{ button.name }}</mat-button-toggle
            >
        </mat-button-toggle-group>
    `,
    styles: [`:host {
        padding-bottom: 20px;
    }`]
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
