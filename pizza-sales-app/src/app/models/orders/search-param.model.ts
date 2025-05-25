export interface SearchParam {
  from?: Date | null;
  to?: Date | null;
  orderNumber?: string;
  pageSize: number;
  sortBy: string;
  sortDirection?: 'asc' | 'desc';
  pageNumber?: number;
}
