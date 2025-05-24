import { Injectable, TemplateRef } from '@angular/core';

export interface Toast {
  template?: TemplateRef<any>;
  message?: string;
  classname?: string;
  type?: 'success' | 'info' | 'warning' | 'danger';
  delay?: number;
}

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  toasts: Toast[] = [];

  show(toastOrMessage: Toast | string, type: Toast['type'] = 'info', delay = 5000) {
    if (typeof toastOrMessage === 'string') {
      this.toasts.push({
        message: toastOrMessage,
        type,
        classname: `bg-${type} text-white`,
        delay
      });
    } else {
      this.toasts.push(toastOrMessage);
    }
  }

  remove(toast: Toast) {
    this.toasts = this.toasts.filter((t) => t !== toast);
  }

  clear() {
    this.toasts.splice(0, this.toasts.length);
  }
}
