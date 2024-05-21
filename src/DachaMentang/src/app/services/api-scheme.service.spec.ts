import { TestBed } from '@angular/core/testing';

import { ApiSchemeService } from './api-scheme.service';

describe('ApiSchemeService', () => {
  let service: ApiSchemeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiSchemeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
