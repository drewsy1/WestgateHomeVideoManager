import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterButtonGroupComponent } from './filter-button-group.component';

describe('FilterButtonGroupComponent', () => {
    let component: FilterButtonGroupComponent;
    let fixture: ComponentFixture<FilterButtonGroupComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [FilterButtonGroupComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(FilterButtonGroupComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
