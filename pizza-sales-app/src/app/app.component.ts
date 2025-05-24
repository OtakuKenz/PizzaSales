import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { ToastContainerComponent } from "./components/toast-container/toast-container.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterModule, ToastContainerComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  
}
