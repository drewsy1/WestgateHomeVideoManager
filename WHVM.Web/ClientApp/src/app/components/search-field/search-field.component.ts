import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
    selector: 'app-search-field',
    templateUrl: './search-field.component.html',
    styleUrls: ['./search-field.component.scss']
})
export class SearchFieldComponent implements OnInit {
    @Output() valueChanges: EventEmitter<any> = new EventEmitter<any>();
    searchControl = new FormControl('');

    constructor() {
        this.searchControl.valueChanges.subscribe(
            (newData: string) =>
                this.valueChanges.emit(newData)
        );
    }

    ngOnInit() {
        this.searchControl.reset();
    }
}
