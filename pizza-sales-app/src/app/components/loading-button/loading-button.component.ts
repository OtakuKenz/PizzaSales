import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-loading-button',
  imports: [],
  templateUrl: './loading-button.component.html',
  styleUrl: './loading-button.component.css'
})
export class LoadingButtonComponent {
  @Input()
  text: string = 'Loading...';
}
