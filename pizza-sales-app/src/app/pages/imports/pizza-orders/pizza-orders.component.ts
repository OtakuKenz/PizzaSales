import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FileInputComponent } from '../../../components/file-input/file-input.component';
import { ImportDataService } from '../../../services/import-data.service';
import { ToastService } from '../../../services/toast.service';
import { concatMap } from 'rxjs/operators';
import { LoadingButtonComponent } from "../../../components/loading-button/loading-button.component";

@Component({
  selector: 'app-pizza-orders',
  imports: [CommonModule, ReactiveFormsModule, FileInputComponent, LoadingButtonComponent],
  templateUrl: './pizza-orders.component.html',
  styleUrl: './pizza-orders.component.css'
})
export class PizzaOrdersComponent {
  form: FormGroup;

  isSaving: boolean = false;

  constructor(private fb: FormBuilder,
    private importDataService: ImportDataService,
    private toastService: ToastService,
  ) {
    this.form = this.fb.group({
      orderCSV: [null, Validators.required],
      orderDetailCSV: [null, Validators.required]
    });
  }

  onSubmit() {
    this.isSaving = true;

    const orderFile: File | null = this.form.value.orderCSV;
    const orderDetailFile: File | null = this.form.value.orderDetailCSV;

    let isFirstRequestValid = false;
    if (this.form.valid && orderFile && orderDetailFile) {
      this.importDataService.uploadOrder(orderFile).pipe(
        concatMap((orderRes) => {
          isFirstRequestValid = true;
          let message = `<b>Pizza Orders Import</b><br>Inserted: ${orderRes.inserted}<br>` +
            `Duplicates: ${orderRes.duplicates}`;
          this.toastService.show(message, `${orderRes.inserted == 0 ? 'info' : 'success'}`, 8000);
          return this.importDataService.uploadOrderDetail(orderDetailFile);
        })
      ).subscribe({
        next: (orderDetailRes) => {
          let message = `<b>Pizza Order Details Import</b><br>Inserted: ${orderDetailRes.inserted}<br>` +
            `Duplicates: ${orderDetailRes.duplicates}`;
          this.toastService.show(message, `${orderDetailRes.inserted == 0 ? 'info' : 'success'}`, 8000);
          this.isSaving = false; 
        },
        error: (err) => {
          if (isFirstRequestValid) {
            this.toastService.show(`<b>Pizza Order Details Import</b><br>Error: ${err.error.message || 'An error occurred while importing pizza order details.'}`, 'danger', 8000);
          } else {
            this.toastService.show(`<b>Pizza Order Details</b><br>Error: ${err.error.message || 'An error occurred while importing pizza orders. Skipping import of pizza order details.'}`, 'danger', 8000);
          }
          this.isSaving = false; 
        }
      });
    }
  }
}
