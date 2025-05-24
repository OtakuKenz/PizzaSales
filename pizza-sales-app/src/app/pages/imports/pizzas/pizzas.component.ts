import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FileInputComponent } from '../../../components/file-input/file-input.component';
import { ImportDataService } from '../../../services/import-data.service';
import { ToastService } from '../../../services/toast.service';
import { LoadingButtonComponent } from "../../../components/loading-button/loading-button.component";

@Component({
  selector: 'app-pizzas',
  imports: [CommonModule, ReactiveFormsModule, FileInputComponent, LoadingButtonComponent],
  templateUrl: './pizzas.component.html',
  styleUrl: './pizzas.component.css'
})
export class PizzasComponent {
  isSaving: boolean = false;
  form: FormGroup;

  constructor(private fb: FormBuilder,
    private importDataService: ImportDataService,
    private toastService: ToastService,
  ) {
    this.form = this.fb.group({
      csvFile: [null, Validators.required]
    });
  }

  onSubmit() {
    this.isSaving = true;
    const file: File | null = this.form.value.csvFile;
    if (this.form.valid && file) {
      const title = '<b>Pizza Import</b><br>';
      this.importDataService.uploadPizza(file).subscribe({
        next: (res) => {
          let message = `${title}Inserted: ${res.inserted}<br>` +
            `Duplicates: ${res.duplicates}`;
          this.toastService.show(message, `${res.inserted == 0 ? 'info' : 'success'}`);
          this.isSaving = false;
        },
        error: (err) => {
          this.toastService.show(`${title}Error: ${err.error.message || 'An error occurred while importing pizza types.'}`, 'danger');
          this.isSaving = false;
        },
      });
    }
  }
}
