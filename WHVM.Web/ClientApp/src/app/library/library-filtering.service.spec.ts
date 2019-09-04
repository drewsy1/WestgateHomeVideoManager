import { TestBed } from '@angular/core/testing';

import { LibraryFilteringService } from './library-filtering.service';

describe('LibraryFilteringService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LibraryFilteringService = TestBed.get(LibraryFilteringService);
    expect(service).toBeTruthy();
  });
});
