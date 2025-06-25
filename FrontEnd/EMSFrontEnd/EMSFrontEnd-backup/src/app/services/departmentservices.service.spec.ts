import { TestBed } from '@angular/core/testing';

import { DepartmentservicesService } from './departmentservices.service';

describe('DepartmentservicesService', () => {
  let service: DepartmentservicesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DepartmentservicesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
