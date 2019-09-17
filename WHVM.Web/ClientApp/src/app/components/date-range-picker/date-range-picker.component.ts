import { AfterViewInit, Component, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { combineLatest } from 'rxjs';
import { startWith } from 'rxjs/operators';

@Component({
    selector: 'app-date-range-picker',
    template: `
        <mat-form-field>
            <label>
                <input
                        matInput
                        [matDatepicker]="startDatePicker //noinspection InvalidExpressionResultType"
                        placeholder="Start Date"
                        [formControl]="datePickerStart"
                />
            </label>
            <mat-datepicker-toggle
                    matSuffix
                    [for]="startDatePicker"
            ></mat-datepicker-toggle>
            <mat-datepicker #startDatePicker></mat-datepicker>
        </mat-form-field>
        <br />
        <mat-form-field>
            <label>
                <input
                        matInput
                        [matDatepicker]="endDatePicker //noinspection InvalidExpressionResultType"
                        placeholder="End Date"
                        [formControl]="datePickerEnd"
                />
            </label>
            <mat-datepicker-toggle
                    matSuffix
                    [for]="endDatePicker"
            ></mat-datepicker-toggle>
            <mat-datepicker #endDatePicker></mat-datepicker>
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
export class DateRangePickerComponent implements OnInit, AfterViewInit {
    @Input() StartDate: Date;
    @Input() EndDate: Date;

    datePickerStart = new FormControl(this.StartDate);
    datePickerEnd = new FormControl(this.EndDate);

    @Output() valueChanges = combineLatest(
        this.datePickerStart.valueChanges.pipe(
            startWith(this.datePickerStart.value)
        ),
        this.datePickerEnd.valueChanges.pipe(
            startWith(this.datePickerEnd.value)
        )
    );

    constructor() {
    }

    ngOnInit() {
    }

    ngAfterViewInit(): void {
        this.valueChanges.subscribe(x => console.log(x));
    }
}
