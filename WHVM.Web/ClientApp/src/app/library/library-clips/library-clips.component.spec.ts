import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LibraryClipsComponent } from './library-clips.component';

describe('LibraryClipsComponent', () => {
    let component: LibraryClipsComponent;
    let fixture: ComponentFixture<LibraryClipsComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [LibraryClipsComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(LibraryClipsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
