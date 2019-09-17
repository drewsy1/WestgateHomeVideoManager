import { Component, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
    selector: 'app-date-picker',
    template: `
        <mat-form-field>
            <label>
                <input
                        matInput
                        [matDatepicker]="picker //noinspection InvalidExpressionResultType"
                        placeholder="Start Date"
                />
            </label>
            <mat-datepicker-toggle
                    matSuffix
                    [for]="picker"
            ></mat-datepicker-toggle>
            <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>
    `,
    styles: [
            `
            mat-form-field {
                width: 100%;
            }
        `
    ]
})
export class DatePickerComponent implements OnInit {
    datePickerControl = new FormControl(new Date());
    @Output() valueChanges = this.datePickerControl.valueChanges;

    constructor() {
    }

    ngOnInit() {
    }
}
