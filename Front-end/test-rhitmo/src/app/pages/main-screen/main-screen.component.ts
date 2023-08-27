import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CustomersToGridResponse } from 'src/app/objects/responses/CustomersToGridResponse';
import { BaseService } from 'src/app/services/base/base.service';
import { CustomerService } from 'src/app/services/customer/customer.service';

@Component({
  selector: 'app-main-screen',
  templateUrl: './main-screen.component.html',
  styleUrls: ['./main-screen.component.scss']
})
export class MainScreenComponent implements OnInit  {

  filteredCustomer!: string;
  customersToGridList!: CustomersToGridResponse[];

  constructor(public baseService: BaseService,
    private customerService: CustomerService,
    private toastrService: ToastrService) {
  }

  ngOnInit(): void {
    this.loadGrid()
  }

  private loadGrid(): void {
    this.customerService.loadGrid()
      .subscribe({
        next: (result) => {

          if (result.success) {
            this.toastrService.success(result.message, 'info');
            this.customersToGridList = result.data;
          }
          else {
            this.toastrService.info(result.message, 'info');
          }
        },
        error: (error) => {
          this.toastrService.error(`Ocorreu um erro durante a comunicação com o serviço. Status: ${error.status}`);
        },
      });
  }

  teste() {
    console.log(this.customersToGridList);
  }

}
