import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'home',
    loadComponent: () => import('./pages/home/home.component').then(m => m.HomeComponent),
  },
  {
    path: 'imports',
    loadComponent: () => import('./pages/imports/imports.component').then(m => m.ImportsComponent),
    children: [
      {
        path: 'pizzas',
        loadComponent: () => import('./pages/imports/pizzas/pizzas.component').then(m => m.PizzasComponent)
      },
      {
        path: 'orders',
        loadComponent: () => import('./pages/imports/pizza-orders/pizza-orders.component').then(m => m.PizzaOrdersComponent)
      },
      {
        path: 'pizza-types',
        loadComponent: () => import('./pages/imports/pizza-types/pizza-types.component').then(m => m.PizzaTypesComponent)
      }
    ]
  },
  {
    path: 'orders',
    loadComponent: () => import('./pages/orders/orders.component').then(m => m.OrdersComponent),
    children: [
      {
        path: 'search',
        loadComponent: () => import('./pages/orders/search/search.component').then(m => m.SearchComponent)
      },
      {
        path: 'detail/:id', 
        loadComponent: () => import('./pages/orders/detail/detail.component').then(m => m.DetailComponent)
      }
    ]
  }
];
