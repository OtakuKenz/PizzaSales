import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FileInputComponent } from '../../../components/file-input/file-input.component';
import { ImportDataService } from '../../../services/import-data.service';
import { ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-pizzas',
  imports: [CommonModule, ReactiveFormsModule, FileInputComponent],
  templateUrl: './pizzas.component.html',
  styleUrl: './pizzas.component.css'
})
export class PizzasComponent {
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
    const file: File | null = this.form.value.csvFile;
    if (this.form.valid && file) {
      const title = '<b>Pizza Import</b><br>';
      this.importDataService.uploadPizza(file).subscribe({
        next: (res) => {
          let message = `${title}Inserted: ${res.inserted}<br>` +
            `Duplicates: ${res.duplicates}`;
          this.toastService.show(message, `${res.inserted == 0 ? 'info' : 'success'}`);
        },
        error: (err) => this.toastService.show(`${title}Error: ${err.error.message || 'An error occurred while importing pizza types.'}`, 'danger'),
      });
    }
  }
}
