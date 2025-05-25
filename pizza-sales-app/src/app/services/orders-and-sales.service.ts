import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { SearchParam as searchQuery } from '../models/orders/search-param.model';
import { PaginatedResponse } from '../models/orders/paginated-response.model';
import { OrderDetail } from '../models/orders/order-detail.model';
import { TopPizza } from '../models/home/top-pizza.model';

@Injectable({
  providedIn: 'root'
})
export class OrdersAndSalesService {
  private apiRoot = environment.apiUrl; 

  constructor(private http: HttpClient) { }

  getTotalSales(){
    return this.http.get<number>(`${this.apiRoot}/Pizza/total-sales`);
  }

  getOrders(searchQuery: searchQuery) {
    const params: any = {
      pageSize: searchQuery.pageSize,
      sortBy: searchQuery.sortBy,
      sortDirection: searchQuery.sortDirection || 'asc',
      pageNumber: searchQuery.pageNumber || 1,
    };

    if (searchQuery.orderNumber && searchQuery.orderNumber.trim() !== '') {
      params.orderNumber = searchQuery.orderNumber;
    }

    if (searchQuery.from) {
      params.from = searchQuery.from;
    }
    if (searchQuery.to) {
      params.to = searchQuery.to;
    }
    
    return this.http.get<PaginatedResponse>(`${this.apiRoot}/Order/All`, { params });
  }

  getOrder(orderId: string) {
    return this.http.get<OrderDetail>(`${this.apiRoot}/OrderDetail/${orderId}`);
  }

  getTopPizzas() {
    return this.http.get<TopPizza[]>(`${this.apiRoot}/Pizza/sales-report`);
  }

}
