import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CustomerSharedData } from 'src/app/objects/interfaces/generics';
import { CustomersToGridResponse } from 'src/app/objects/responses/CustomersToGridResponse';
import { BaseService } from 'src/app/services/base/base.service';
import { CustomerService } from 'src/app/services/customer/customer.service';
import { SharedDataService } from 'src/app/services/shared-data/shared-data.service';

@Component({
  selector: 'app-main-screen',
  templateUrl: './main-screen.component.html',
  styleUrls: ['./main-screen.component.scss']
})
export class MainScreenComponent implements OnInit  {

  filteredCustomer!: string;
  customersToGridList!: CustomersToGridResponse[];
  customerName!: string;
  customerEmail!: string;
  customerCpf!: string;

  constructor(private baseService: BaseService,
    private customerService: CustomerService,
    private toastrService: ToastrService,
    private sharedDataService: SharedDataService
    ) {
  }

  ngOnInit(): void {
    this.loadGrid()
  }

  private loadGrid(): void {
    this.customerService.loadGrid()
      .subscribe({
        next: (result) => {
          if (result.success) {
            this.toastrService.success(result.message);
            this.customersToGridList = result.data;
          }
          else {
            this.toastrService.info(result.message);
          }
        },
        error: (error) => {
          this.toastrService.error(`Ocorreu um erro durante a comunicação com o serviço. Status: ${error.status}`);
        },
      });
  }

  registerCustomer(customer?: CustomersToGridResponse) {
    let dataToShare: CustomerSharedData;

    if (!customer && (this.customerName || this.customerEmail || this.customerCpf)) {
      dataToShare = {
        isEditing: false,
        name: this.customerName,
        email: this.customerEmail,
        cpf: this.customerCpf
      }

      this.sharedDataService.setData(dataToShare);
    }
    else if (customer) {
      dataToShare = {
        isEditing: true,
        name: customer.name,
        email: customer.email,
        cpf: customer.cpf,
        address: customer.address,
        state: customer.state,
        city: customer.city,
        cep: customer.cep,
        paymentMethod: customer.paymentMethod,
        insertedAt: customer.insertedAt,
        creditCardDto: customer.creditCardDto
      }

      this.sharedDataService.setData(dataToShare);
    }

    this.baseService.navigate('cadastro');
  }

}
