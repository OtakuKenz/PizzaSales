import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImportDataService {
  private apiRoot = environment.apiUrl; 

  constructor(private http: HttpClient) {}

  /**
   * Uploads a file containing pizza type data to the server for import.
   *
   * @param file - The file to be uploaded, typically containing pizza type information.
   * @returns An Observable emitting the server's response to the import request.
   */
  uploadPizzaTypes(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post(`${this.apiRoot}/pizzaType/Import`, formData);
  }

  /**
   * Uploads a pizza data file to the server for import.
   *
   * @param file - The file containing pizza data to be uploaded.
   * @returns An Observable emitting the server's response to the upload request.
   */
  uploadPizza(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post(`${this.apiRoot}/pizza/Import`, formData);
  }

  /**
   * Uploads an order file to the server for import.
   *
   * @param file - The file containing order data to be uploaded.
   * @returns An Observable emitting the server's response to the import request.
   */
  uploadOrder(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post(`${this.apiRoot}/Order/Import`, formData);
  }

  /**
   * Uploads an order detail file to the server for import.
   *
   * @param file - The file containing order details to be uploaded.
   * @returns An Observable emitting the server's response to the upload request.
   */
  uploadOrderDetail(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post(`${this.apiRoot}/OrderDetail/Import`, formData);
  }

  // Add similar methods for pizzas and orders if needed
}
