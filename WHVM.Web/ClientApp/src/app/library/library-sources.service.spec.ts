import { TestBed } from '@angular/core/testing';

import { LibrarySourcesService } from './library-sources.service';

describe('LibrarySourcesService', () => {
    beforeEach(() => TestBed.configureTestingModule({}));

    it('should be created', () => {
        const service: LibrarySourcesService = TestBed.get(LibrarySourcesService);
        expect(service).toBeTruthy();
    });
});
