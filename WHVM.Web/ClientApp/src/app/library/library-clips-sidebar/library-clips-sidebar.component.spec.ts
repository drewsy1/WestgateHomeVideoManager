import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LibraryClipsSidebarComponent } from './library-clips-sidebar.component';

describe('LibraryClipsSidebarComponent', () => {
    let component: LibraryClipsSidebarComponent;
    let fixture: ComponentFixture<LibraryClipsSidebarComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [LibraryClipsSidebarComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(LibraryClipsSidebarComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
