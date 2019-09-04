import { Component, Input, OnInit } from '@angular/core';

@Component({
    selector: 'app-page-header',
    template: `
        <div class="component-page-header library-page-header">
            <h1 [innerText]="pageTitle"></h1>
        </div>
    `,
    styleUrls: ['./page-header.component.scss']
})
export class PageHeaderComponent implements OnInit {
    @Input() pageTitle: string;
    constructor() {}

    ngOnInit() {}
}
