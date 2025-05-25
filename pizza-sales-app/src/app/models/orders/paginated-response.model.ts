export interface PaginatedResponse{
  totalRecords: number;
  pageSize: number;
  pageNumber: number;
  data: any[];
}