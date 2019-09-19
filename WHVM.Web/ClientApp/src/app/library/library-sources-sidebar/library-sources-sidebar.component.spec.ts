import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LibrarySourcesSidebarComponent } from './library-sources-sidebar.component';

describe('LibrarySourcesSidebarComponent', () => {
    let component: LibrarySourcesSidebarComponent;
    let fixture: ComponentFixture<LibrarySourcesSidebarComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [LibrarySourcesSidebarComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(LibrarySourcesSidebarComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
