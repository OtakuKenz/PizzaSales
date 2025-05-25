import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { OrdersAndSalesService } from '../../../services/orders-and-sales.service';
import { OrderDetail } from '../../../models/orders/order-detail.model';
import { CommonModule } from '@angular/common';
import { ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.css',
  imports: [CommonModule],
})
export class DetailComponent implements OnInit {
  orderDetail?: OrderDetail;

  constructor(
    private route: ActivatedRoute,
    private ordersService: OrdersAndSalesService,
    private router: Router,
    private toastService: ToastService
  ) { }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.ordersService.getOrder(id).subscribe(detail => {
        this.orderDetail = detail;
      }, error => {
        this.toastService.show(`Error fetching order details<br>${error.message}`, 'danger');
        this.router.navigate(['/orders/search']);
      }
      );
    } else {
      this.toastService.show('Order ID is missing', 'danger');
      this.router.navigate(['/orders/search']);
    }
  }
}
