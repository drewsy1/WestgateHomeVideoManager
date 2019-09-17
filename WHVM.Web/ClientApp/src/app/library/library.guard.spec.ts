import { inject, TestBed } from '@angular/core/testing';

import { LibraryGuard } from './library.guard';

describe('LibraryGuard', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [LibraryGuard]
        });
    });

    it('should ...', inject([LibraryGuard], (guard: LibraryGuard) => {
        expect(guard).toBeTruthy();
    }));
});
