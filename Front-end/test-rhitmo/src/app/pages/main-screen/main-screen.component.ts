import { Component } from '@angular/core';
import { CustomersToGridResponse } from 'src/app/objects/responses/CustomersToGridResponse';
import { BaseService } from 'src/app/services/base/base.service';

@Component({
  selector: 'app-main-screen',
  templateUrl: './main-screen.component.html',
  styleUrls: ['./main-screen.component.scss']
})
export class MainScreenComponent  {

  filteredCustomer!: string;
  
  customersToGridList: CustomersToGridResponse[] = [
    {
      name: "John Doe",
      email: "john@example.com",
      cpf: "123.456.789-00",
      insertedAt: "2023-08-27",
    },
    {
      name: "Jane Smith",
      email: "jane@example.com",
      cpf: "987.654.321-00",
      insertedAt: "2023-08-27",
    },
    {
      name: "Alice Johnson",
      email: "alice@example.com",
      cpf: "555.555.555-00",
      insertedAt: "2023-08-27",
    },
    {
      name: "Bob Brown",
      email: "bob@example.com",
      cpf: "777.888.999-00",
      insertedAt: "2023-08-27",
    },
    {
      name: "Eve White",
      email: "eve@example.com",
      cpf: "111.222.333-00",
      insertedAt: "2023-08-27",
    },
    {
      name: "David Johnson",
      email: "david@example.com",
      cpf: "444.444.444-00",
      insertedAt: "2023-08-27",
    },
    {
      name: "Sarah Davis",
      email: "sarah@example.com",
      cpf: "888.888.888-00",
      insertedAt: "2023-08-27",
    },
    {
      name: "Michael Smith",
      email: "michael@example.com",
      cpf: "999.999.999-00",
      insertedAt: "2023-08-27",
    },
    {
      name: "Olivia Brown",
      email: "olivia@example.com",
      cpf: "333.333.333-00",
      insertedAt: "2023-08-27",
    },
    {
      name: "William Taylor",
      email: "william@example.com",
      cpf: "666.666.666-00",
      insertedAt: "2023-08-27",
    },
  ];

  constructor(public baseService: BaseService) {

  }

}
