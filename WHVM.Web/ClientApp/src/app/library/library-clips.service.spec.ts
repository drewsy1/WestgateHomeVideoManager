import { TestBed } from '@angular/core/testing';

import { LibraryClipsService } from './library-clips.service';

describe('LibraryClipsService', () => {
    beforeEach(() => TestBed.configureTestingModule({}));

    it('should be created', () => {
        const service: LibraryClipsService = TestBed.get(LibraryClipsService);
        expect(service).toBeTruthy();
    });
});
