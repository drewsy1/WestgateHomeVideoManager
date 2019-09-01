import { TestBed } from '@angular/core/testing';

import { CookieManagerService } from './cookie-manager.service';

describe('CookieManagerService', () => {
    beforeEach(() => TestBed.configureTestingModule({}));

    it('should be created', () => {
        const service: CookieManagerService = TestBed.get(CookieManagerService);
        expect(service).toBeTruthy();
    });
});
