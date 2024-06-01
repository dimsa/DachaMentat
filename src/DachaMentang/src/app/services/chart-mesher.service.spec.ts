import { TestBed } from '@angular/core/testing';

import { ChartMesherService } from './chart-mesher.service';

describe('ChartMesherService', () => {
  let service: ChartMesherService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ChartMesherService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
