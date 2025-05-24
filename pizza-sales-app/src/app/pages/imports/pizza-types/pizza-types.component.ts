import { Component, TemplateRef, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FileInputComponent } from '../../../components/file-input/file-input.component';
import { ImportDataService } from '../../../services/import-data.service';
import { Toast, ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-pizza-types',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FileInputComponent],
  templateUrl: './pizza-types.component.html',
  styleUrls: ['./pizza-types.component.css']
})
export class PizzaTypesComponent {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
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
      const title = '<b>Pizza Types Import</b><br>';
      this.importDataService.uploadPizzaTypes(file).subscribe({
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
