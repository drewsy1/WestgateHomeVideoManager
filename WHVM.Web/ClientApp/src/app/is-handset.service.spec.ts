import { TestBed } from '@angular/core/testing';

import { IsHandsetService } from './is-handset.service';

describe('IsHandsetService', () => {
    beforeEach(() => TestBed.configureTestingModule({}));

    it('should be created', () => {
        const service: IsHandsetService = TestBed.get(IsHandsetService);
        expect(service).toBeTruthy();
    });
});
