export interface OrderSummary {
  orderId: number;
  date: string;
  time: string;
  orderTotal: number;
}

export interface PizzaDetail {
  pizza: string;
  ingredients: string[];
  price: number;
  size: string;
  quantity: number;
  subtotal: number;
}

export interface OrderDetail {
  orderSummary: OrderSummary;
  pizzas: PizzaDetail[];
}