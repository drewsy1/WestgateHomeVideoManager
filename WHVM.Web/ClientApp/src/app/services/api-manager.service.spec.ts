import { TestBed } from '@angular/core/testing';

import { ApiManagerService } from './api-manager.service';

describe('ApiManagerService', () => {
    beforeEach(() => TestBed.configureTestingModule({}));

    it('should be created', () => {
        const service: ApiManagerService = TestBed.get(ApiManagerService);
        expect(service).toBeTruthy();
    });
});
