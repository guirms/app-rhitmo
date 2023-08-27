import { Injectable } from '@angular/core';
import { CustomerSharedData } from 'src/app/objects/interfaces/generics';

@Injectable({
  providedIn: 'root'
})
export class SharedDataService {

  data!: CustomerSharedData;

  setData(data: CustomerSharedData) {
    this.data = data;
  }

  getData() {
    return this.data;
  }
}
