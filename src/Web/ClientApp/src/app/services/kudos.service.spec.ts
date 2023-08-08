import { TestBed } from '@angular/core/testing';

import { KudosService } from './kudos.service';

describe('KudosService', () => {
  let service: KudosService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(KudosService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
