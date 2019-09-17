import { AfterViewInit, Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { combineLatest, Observable, of } from 'rxjs';
import { ButtonToggle, ButtonToggleGroupComponent } from '../../../components/button-toggle-group/button-toggle-group.component';
import { delay, map, startWith } from 'rxjs/operators';
import { union } from 'lodash';
import { ApiManagerService } from '../../../services/api-manager.service';

@Component({
    selector: 'app-filter-button-group',
    template: `
        <app-button-toggle-group
                name="sourceFormats"
                [buttons]="this.sourceFormatButtons$ | async"
        ></app-button-toggle-group>
    `,
    styleUrls: ['./filter-button-group.component.scss']
})
export class FilterButtonGroupComponent implements OnInit, AfterViewInit {
    @ViewChild(ButtonToggleGroupComponent, { static: false })
    private buttonToggleGroup: ButtonToggleGroupComponent;

    @Output() valueChanges: EventEmitter<string> = new EventEmitter<string>();

    value = '';

    sourceFormatButtonAll$: Observable<ButtonToggle[]> = of([
        { value: '', name: 'All', checked: true }
    ]);
    sourceFormatButtons$: Observable<ButtonToggle[]>;

    constructor(
        private apiManagerService: ApiManagerService
    ) {
        const sourceFormatsRetrieved$ = this.apiManagerService.getSourceFormatsSubject.pipe(
            startWith(this.apiManagerService.sourceFormats),
            map(x =>
                x.map(
                    newSourceFormat =>
                        ({
                            value: newSourceFormat.sourceFormatId.toString(),
                            name: newSourceFormat.sourceFormatName
                        } as ButtonToggle)
                )
            )
        );

        this.sourceFormatButtons$ = combineLatest(
            this.sourceFormatButtonAll$,
            sourceFormatsRetrieved$
        ).pipe(
            delay(0),
            map(sourceFormat => union(...sourceFormat))
        );
    }

    ngOnInit() {
    }

    ngAfterViewInit(): void {
        this.buttonToggleGroup.valueChanges.subscribe(x =>
            this.valueChanges.emit(x)
        );
    }
}
