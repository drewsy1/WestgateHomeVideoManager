import { TestBed } from '@angular/core/testing';

import { TestHttpMockService } from './test-http-mock.service';

describe('TestHttpMockService', () => {
    beforeEach(() => TestBed.configureTestingModule({}));

    it('should be created', () => {
        const service: TestHttpMockService = TestBed.get(TestHttpMockService);
        expect(service).toBeTruthy();
    });
});
