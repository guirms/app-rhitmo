import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { NoContentResponse } from 'src/app/objects/interfaces/generics';
import { AddCustomerRequest } from 'src/app/objects/requests/AddCustomerRequest ';
import { DefaultResponse } from 'src/app/objects/responses/CustomersToGridResponse';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpClient) { }

  loadGrid(): Observable<DefaultResponse> {
    return this.http.get<DefaultResponse>(environment.apiUrl + 'Customer/GetCustomersToGrid');
  }

  saveCustomer(saveCustomerRequest: AddCustomerRequest):Observable<NoContentResponse> {
    return this.http.post<NoContentResponse>(environment.apiUrl + 'Customer/SaveCustomer', saveCustomerRequest);
  }

  updateCustomer(updateCustomerRequest: AddCustomerRequest):Observable<NoContentResponse> {
    return this.http.post<NoContentResponse>(environment.apiUrl + 'Customer/UpdateCustomer', updateCustomerRequest);
  }

  deleteCustomer(customerId: number):Observable<NoContentResponse>  {
    const params = new HttpParams().set('customerId', customerId);

    return this.http.delete<NoContentResponse>(environment.apiUrl + 'Customer/DeleteCustomer', { params });
  }
}
