import { Component, forwardRef, Input } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-file-input',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './file-input.component.html',
  styleUrls: ['./file-input.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => FileInputComponent),
      multi: true
    }
  ]
})
export class FileInputComponent implements ControlValueAccessor {
  @Input() accept = '.csv';
  @Input() label = 'Choose file';
  @Input() id = 'fileInput';

  file: File | null = null;
  disabled = false;

  private onChange: (file: File | null) => void = () => {};
  private onTouched: () => void = () => {};

  writeValue(file: File | null): void {
    this.file = file;
  }

  registerOnChange(fn: (file: File | null) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  onFileChange(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      if (file.type === 'text/csv' || file.name.endsWith('.csv')) {
        this.file = file;
        this.onChange(file);
      } else {
        this.file = null;
        this.onChange(null);
        input.value = '';
        alert('Please select a valid CSV file.');
      }
    } else {
      this.file = null;
      this.onChange(null);
    }
    this.onTouched();
  }
}
