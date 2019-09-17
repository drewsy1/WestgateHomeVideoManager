import { AfterViewInit, Component, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
    selector: 'app-search-field',
    template: `
        <mat-form-field>
            <label>
                <input
                        matInput
                        placeholder="Search"
                        type="text"
                        [formControl]="searchControl"
                />
            </label>
            <button
                    mat-icon-button
                    matSuffix
                    (click)="searchControl.reset()"
                    [attr.aria-label]="'Clear search'"
            >
                <mat-icon>{{ 'clear' }}</mat-icon>
            </button>
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
export class SearchFieldComponent implements OnInit, AfterViewInit {
    searchControl = new FormControl('');
    @Output() valueChanges = this.searchControl.valueChanges;

    constructor() {
    }

    ngOnInit() {

    }

    ngAfterViewInit(): void {
    }
}
