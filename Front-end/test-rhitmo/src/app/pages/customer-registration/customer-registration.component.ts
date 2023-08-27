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

  registerForm!: FormGroup;
  name!: string;
  email!: string;
  cpf!: string;
  cpfMaxLength = 11;
  cepMaxLength = 8;
  address!: string;
  state!: string | null;
  cep!: string;
  city!: string | null;
  cityDictionary: { [key: number]: string } = {};
  cityPlaceholder = "Selecione o estado antes";

  readonly stateDictionary: { [key: number]: string } = {};

  constructor(private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    public baseService: BaseService,
    public customerService: CustomerService) {

    const stateNames: string[] = environment.brazilLocations.States.map(s => s.Name)
      .sort();

    for (let i = 0; i < stateNames.length; i++) {
      this.stateDictionary[i + 1] = stateNames[i];
    }

    const cityNames: string[] = environment.brazilLocations.States
      .flatMap(state => state.Cities)
      .sort();

    for (let i = 0; i < cityNames.length; i++) {
      this.cityDictionary[i + 1] = cityNames[i];
    }
  }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      cpf: ['', [Validators.required]],
      address: ['', [Validators.required]],
      state: [0, [Validators.required]],
      cep: ['', [Validators.required]],
      city: [0, [Validators.required]],
      cardholderName: ['', []],
      cardNumber: ['', []],
      cardExpirationDate: ['', []],
      cardSecurityCode: ['', []]
    });
  }

  registerClient(): void {
  }

  validateField(fieldName: string): boolean {
    const formField = this.registerForm.get(fieldName);

    return (formField?.invalid && (formField?.dirty || formField?.touched) && !formField?.errors?.email) ?? false;
  }

  validateEmail(): boolean {
    const formField = this.registerForm.get('email');

    return (formField?.errors?.email && (formField?.dirty || formField?.touched)) ?? false;
  }

  validateCpf(): boolean {
    const formField = this.registerForm.get('cpf');

    if (formField) {
      return (this.cpf?.length !== 14 && formField?.touched) ?? false;
    }

    return false;
  }

  validateState(): boolean {
    const formField = this.registerForm.get('state');

    return (!this.state && formField?.touched) ?? false;
  }

  validateCity(): boolean {
    const formField = this.registerForm.get('city');

    return (!this.city && formField?.touched) ?? false;
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
      this.clearCity();
      this.state = null;
    }
    else {
      this.state = target.value;
      this.cityDictionary = {};

      const filteredState = environment.brazilLocations.States
        .filter(s => s.Name === this.stateDictionary[Number(this.state)]);

      if (this.city) {
        const cityExists = filteredState
          .some(c => c.Cities.some(c => c === this.cityDictionary[Number(this.city)]));

        if (!cityExists) {
          this.city = null;
        }
      }
      
      this.cityDictionary = {};

      const cityNames: string[] = filteredState
        .flatMap(state => state.Cities)
        .sort();

      for (let i = 0; i < cityNames.length; i++) {
        this.cityDictionary[i + 1] = cityNames[i];
      }
    }
  }

  clearCity(): void {
    this.cityDictionary = {};
    this.city = null;

    const cityNames: string[] = environment.brazilLocations.States
      .flatMap(state => state.Cities)
      .sort();

    for (let i = 0; i < cityNames.length; i++) {
      this.cityDictionary[i + 1] = cityNames[i];
    }
  }

  setCity(event: Event) {
    const target = event.target as HTMLSelectElement;

    if (Number(target.value) === 0) {
      this.city = null;
    }
    else {
      this.city = target.value;
    }
  }

  teste(): void {
    alert(this.state);
  }
}
