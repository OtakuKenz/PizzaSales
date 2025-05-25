import { TestBed } from '@angular/core/testing';

import { OrdersAndSalesService } from './orders-and-sales.service';

describe('OrdersAndSalesService', () => {
  let service: OrdersAndSalesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrdersAndSalesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
