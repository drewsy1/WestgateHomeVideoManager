import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LibrarySourcesComponent } from './library-sources.component';

describe('LibrarySourcesComponent', () => {
    let component: LibrarySourcesComponent;
    let fixture: ComponentFixture<LibrarySourcesComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [LibrarySourcesComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(LibrarySourcesComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
