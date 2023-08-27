import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { BaseService } from 'src/app/services/base/base.service';
import { CustomerService } from 'src/app/services/customer/customer.service';

@Component({
  selector: 'app-customer-registration',
  templateUrl: './customer-registration.component.html',
  styleUrls: ['./customer-registration.component.scss']
})
export class CustomerRegistrationComponent implements OnInit {

  cadastroForm!: FormGroup;
  name!: string;
  email!: string;
  cpf!: string;
  address!: string;

  constructor(private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    public baseService: BaseService,
    public customerService: CustomerService) { }

  ngOnInit(): void {
    this.cadastroForm = this.formBuilder.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      cpf: ['', [Validators.required]],
      address: ['', [Validators.required]],
      state: ['', [Validators.required]],
      zipCode: ['', [Validators.required]],
      city: ['', [Validators.required]],
      cardholderName: ['', []],
      cardNumber: ['', []],
      cardExpirationDate: ['', []],
      cardSecurityCode: ['', []]
    });
  }


  registerClient(): void {

  }

  validateField(fieldName: string): boolean {
    const formField = this.cadastroForm.get(fieldName);

    return (formField?.invalid && (formField?.dirty || formField?.touched) && !formField?.errors?.email) ?? false;
  }

  validateEmail(): boolean {
    const formField = this.cadastroForm.get('email');

    return (formField?.errors?.email && (formField?.dirty || formField?.touched)) ?? false;
  }

  validateCpf(): boolean {
    const formField = this.cadastroForm.get('cpf');

    return (this.cpf.length !== 14 && formField?.touched) ?? false;
  }

  formatCpf(): void {
    const cleanCpf = this.cpf.replace(/\D/g, '');

    this.cpf = cleanCpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/g, "\$1.\$2.\$3-\$4");
  }


  addCharactere(text: string, charactere: string, position: number): string {
    const primeiraParte = text.slice(0, position);
    const segundaParte = text.slice(position);

    return primeiraParte + charactere + segundaParte;
  }
}
