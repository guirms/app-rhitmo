import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
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
}
