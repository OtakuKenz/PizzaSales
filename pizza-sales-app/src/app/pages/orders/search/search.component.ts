import { Component, AfterViewInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DatatableComponent } from '../../../components/datatable/datatable.component';
import { OrdersAndSalesService } from '../../../services/orders-and-sales.service';
import { SearchParam } from '../../../models/orders/search-param.model';
import { ResponseDataMapped } from '../../../models/orders/responseData.model';
import { ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, DatatableComponent],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent {
  form: FormGroup;
  dataTable: any;

  columns = [
    { field: 'orderId', header: 'Order Number', sortable: true, link: true }, 
    { field: 'date', header: 'Order Date', sortable: true },
    { field: 'time', header: 'Time', sortable: false },
    { field: 'orderTotal', header: 'Total', sortable: true }
  ];

  orderSearch: SearchParam = {
    pageSize: 0,
    sortBy: 'orderId'
  }

  orders: ResponseDataMapped[] = [];
  loading = false;
  totalOrders: number = 0;

  constructor(private fb: FormBuilder,
    private orderAndSalesService: OrdersAndSalesService,
    private toastService: ToastService
  ) {
    this.form = this.fb.group({
      from: [null],
      to: [null],
      orderNumber: ['', [Validators.pattern('^[0-9]*$')]],
      pageSize: [10, [Validators.required, Validators.min(10), Validators.max(50)]]
    });
  }

  fetchOrders() {
    this.loading = true;
    this.orderSearch.pageNumber ?? 1;
    this.orderSearch.pageSize ?? 10;
    this.orderSearch.sortBy ?? 'orderDate';
    this.orderSearch.sortDirection ?? 'asc';
    this.orderAndSalesService.getOrders(this.orderSearch).subscribe((res: any) => {
      this.orders = res.data.map((order: any) => ({
        orderId: order.orderId,
        date: order.date ? new Date(order.date).toLocaleDateString('en-US') : '',
        time: order.time,
        orderTotal: `$ ${order.orderTotal}`,
        href: `/orders/detail/${order.orderId}`
      }));
      this.totalOrders = res.totalRecords;
      this.loading = false;
    });
  }

  onPageChange(newPage: number) {
    this.orderSearch.pageNumber = newPage;
    this.fetchOrders();
  }

  onSortChange(sort: { field: string, direction: 'asc' | 'desc' }) {
    this.orderSearch.sortBy = sort.field;
    this.orderSearch.sortDirection = sort.direction;
    this.fetchOrders();
  }

  onSubmit() {
    if (this.form.value.from && this.form.value.to) {
      const fromDate = new Date(this.form.value.from);
      const toDate = new Date(this.form.value.to);
      if (fromDate > toDate) {
        this.toastService.show('From date cannot be later than To date.', 'danger');
        return;
      }
    }
    this.orderSearch.from = this.form.value.from;
    this.orderSearch.orderNumber = this.form.value.orderNumber || '';
    this.orderSearch.to = this.form.value.to;
    this.orderSearch.pageSize = this.form.value.pageSize || 10;
    this.orderSearch.pageNumber = 1;
    this.fetchOrders();
  }
}
