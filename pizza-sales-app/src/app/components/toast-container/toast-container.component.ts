import { Component } from '@angular/core';
import { ToastService } from '../../services/toast.service';
import { CommonModule, NgTemplateOutlet } from '@angular/common';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-toast-container',
  imports: [CommonModule, NgbToastModule, NgTemplateOutlet],
  templateUrl: './toast-container.component.html',
  styleUrl: './toast-container.component.css',
})
export class ToastContainerComponent {
  constructor(public toastService: ToastService) {
  }
}