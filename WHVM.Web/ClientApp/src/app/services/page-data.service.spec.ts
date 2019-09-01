import { TestBed } from '@angular/core/testing';

import { PageDataService } from './page-data.service';

describe('PageDataService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PageDataService = TestBed.get(PageDataService);
    expect(service).toBeTruthy();
  });
});
