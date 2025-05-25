import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { RouterModule } from '@angular/router';

export interface DataTableColumn {
  field: string;
  header: string;
  link?: boolean;    
  sortable?: boolean;
}

@Component({
  imports: [CommonModule, RouterModule],
  selector: 'app-datatable',
  templateUrl: './datatable.component.html',
  styleUrls: ['./datatable.component.css']
})
export class DatatableComponent {
  @Input() columns: DataTableColumn[] = [];
  @Input() data: any[] = [];
  @Input() totalRecords: number = 0;
  @Input() pageSize: number = 10;
  @Input() page: number = 1;
  @Input() loading: boolean = false;

  @Output() pageChange = new EventEmitter<number>();
  @Output() sortChange = new EventEmitter<{ field: string, direction: 'asc' | 'desc' }>();

  sortField: string = '';
  sortDirection: 'asc' | 'desc' = 'asc';

  onSort(col: DataTableColumn) {
    if (!col.sortable) return;
    if (this.sortField === col.field) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortField = col.field;
      this.sortDirection = 'asc';
    }
    this.sortChange.emit({ field: this.sortField, direction: this.sortDirection });
  }

  onPageChange(newPage: number) {
    if (newPage !== this.page) {
      this.pageChange.emit(newPage);
    }
  }

  get totalPages(): number {
    return Math.ceil(this.totalRecords / this.pageSize) || 1;
  }

  get pages(): number[] {
    const pages: number[] = [];
    for (let i = 1; i <= this.totalPages; i++) {
      pages.push(i);
    }
    return pages;
  }

  onClick(clickedId: string){
    
  }
}
