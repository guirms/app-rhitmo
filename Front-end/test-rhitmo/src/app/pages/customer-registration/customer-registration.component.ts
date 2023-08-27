import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { KeyAndValue } from 'src/app/objects/interfaces/generics';
import { BaseService } from 'src/app/services/base/base.service';
import { CustomerService } from 'src/app/services/customer/customer.service';
import { environment } from 'src/environments/environment';

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
  cpfMaxLength = 11;
  cepMaxLength = 8;
  address!: string;
  state!: string | null;
  cep!: string;

  readonly stateDictionary: { [key: number]: string } = {};

  constructor(private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    public baseService: BaseService,
    public customerService: CustomerService) {

    const stateNames = environment.brazilLocations.States.map(s => s.Name);
    for (let i = 0; i < stateNames.length; i++) {
      this.stateDictionary[i + 1] = stateNames[i];
    }
  }

  ngOnInit(): void {
    this.cadastroForm = this.formBuilder.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      cpf: ['', [Validators.required]],
      address: ['', [Validators.required]],
      state: [0, [Validators.required]],
      cep: ['', [Validators.required]],
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

    if (formField) {
      return (this.cpf?.length !== 14 && formField?.touched) ?? false;
    }

    return false;
  }

  validateState(): boolean {
    const formField = this.cadastroForm.get('state');

    return (!this.state && formField?.touched) ?? false;
  }

  validateCep(): boolean {
    return false;
  }

  formatCpf(): void {
    if (this.cpf) {
      const cleanCpf = this.cpf.replace(/\D/g, '');

      if (cleanCpf.length === 11) {
        this.cpfMaxLength = 14;
      }
      else {
        this.cpfMaxLength = 11;
      }

      this.cpf = cleanCpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/g, "\$1.\$2.\$3-\$4");
    }
  }

  formatCep(): void {
    if (this.cep) {
      const cleanCep = this.cep.replace(/\D/g, '');

      if (cleanCep.length === 8) {
        this.cepMaxLength = 12;
      } else {
        this.cepMaxLength = 8;
      }

      this.cep = cleanCep.replace(/(\d{2})(\d{3})(\d{3})/g, '$1.$2 - $3');
    }
  }
  addCharactere(text: string, charactere: string, position: number): string {
    const primeiraParte = text.slice(0, position);
    const segundaParte = text.slice(position);

    return primeiraParte + charactere + segundaParte;
  }

  setState(event: Event) {
    const target = event.target as HTMLSelectElement;

    if (Number(target.value) === 0) {
      this.state = null;
    }
    else {
      this.state = target.value;
    }
  }

  teste(): void {
    alert(this.state);
  }
}
