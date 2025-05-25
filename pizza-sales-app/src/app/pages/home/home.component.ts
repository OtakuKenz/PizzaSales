import { Component, OnInit } from '@angular/core';
import { OrdersAndSalesService } from '../../services/orders-and-sales.service';
import { CommonModule } from '@angular/common';
import { TopPizza } from '../../models/home/top-pizza.model';

@Component({
  selector: 'app-home',
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  constructor(private ordersService: OrdersAndSalesService) {
  }
  ngOnInit(): void {
    this.ordersService.getTotalSales().subscribe((sales: number) => {
      this.totalSales = sales;
    });

    this.ordersService.getTopPizzas().subscribe((pizzas: TopPizza[]) => {
      this.topPizzas = pizzas;
    });
  }

  totalSales = 0; 
  topPizzas: TopPizza[] = [];
}
