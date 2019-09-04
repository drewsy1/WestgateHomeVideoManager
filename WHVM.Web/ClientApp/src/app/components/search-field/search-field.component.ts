import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
    selector: 'app-search-field',
    templateUrl: './search-field.component.html',
    styleUrls: ['./search-field.component.scss']
})
export class SearchFieldComponent implements OnInit {
    searchControl = new FormControl('');
    constructor() {}

    ngOnInit() {
        this.searchControl.reset();
    }
}
