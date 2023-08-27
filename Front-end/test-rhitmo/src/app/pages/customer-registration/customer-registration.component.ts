import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { EPaymentMethod } from 'src/app/objects/enums/EPaymentMethod';
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
  address!: string;
  state!: string | null;
  cep!: string;
  city!: string | null;
  isCreditCard = true;
  cardHolderName!: string;
  cardNumber!: string;
  cardSecurityCode!: string;
  cardExpirationMonth!: string | null;
  cardExpirationYear!: string | null;

  cpfMaxLength = 11;
  cepMaxLength = 8;

  cityDictionary: { [key: number]: string } = {};
  readonly stateDictionary: { [key: number]: string } = {};
  readonly monthDictionary: { [key: number]: string } = {};
  readonly yearDictionary: { [key: number]: string } = {};

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

    const monthNames: string[] = environment.months.sort();

    for (let i = 0; i < monthNames.length; i++) {
      this.monthDictionary[i + 1] = monthNames[i];
    }

    const currentYear = new Date().getFullYear();
    const futureYears = 20;
    const yearNames = Array.from({ length: futureYears }, (_, index) => (currentYear + index).toString()).sort();

    for (let i = 0; i < yearNames.length; i++) {
      this.yearDictionary[i + 1] = yearNames[i];
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
      cardHolderName: ['', [Validators.required]],
      cardExpirationMonth: [0, [Validators.required]],
      cardExpirationYear: [0, [Validators.required]],
      cardNumber: ['', [Validators.required]],
      cardSecurityCode: ['', [Validators.required]]
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

  validateCardNumber(): boolean {
    const formField = this.registerForm.get('cardNumber');

    if (formField) {
      return (this.cardNumber?.length !== 19 && formField?.touched) ?? false;
    }

    return false;
  }

  validateCardSecurityCode(): boolean {
    const formField = this.registerForm.get('cardNumber');

    if (formField) {
      return (this.cardNumber?.length !== 3 && formField?.touched) ?? false;
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

  validateMonth(): boolean {
    const formField = this.registerForm.get('cardExpirationMonth');

    return (!this.cardExpirationMonth && formField?.touched) ?? false;
  }

  validateYear(): boolean {
    const formField = this.registerForm.get('cardExpirationYear');

    return (!this.cardExpirationYear && formField?.touched) ?? false;
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
      } 
      else {
        this.cepMaxLength = 8;
      }

      this.cep = cleanCep.replace(/(\d{2})(\d{3})(\d{3})/g, '$1.$2 - $3');
    }
  }

  formatCardNumber(): void {
    if (this.cardNumber) {
      const cleanCardNumber = this.cardNumber.replace(/\D/g, '');

      const chunkedNumbers = cleanCardNumber.match(/.{1,4}/g);

      if (chunkedNumbers) {
        this.cardNumber = chunkedNumbers.join(' ');
      }
    }
  }


  addCharactere(text: string, charactere: string, position: number): string {
    const primeiraParte = text.slice(0, position);
    const segundaParte = text.slice(position);

    return primeiraParte + charactere + segundaParte;
  }

  setState(event: Event): void {
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

  setCity(event: Event): void {
    const target = event.target as HTMLSelectElement;

    if (Number(target.value) === 0) {
      this.city = null;
    }
    else {
      this.city = target.value;
    }
  }

  setCardExpirationMonth(event: Event): void {
    const target = event.target as HTMLSelectElement;

    if (Number(target.value) === 0) {
      this.cardExpirationMonth = null;
    }
    else {
      this.cardExpirationMonth = target.value;
    }
  }

  setCardExpirationYear(event: Event): void {
    const target = event.target as HTMLSelectElement;

    if (Number(target.value) === 0) {
      this.cardExpirationYear = null;
    }
    else {
      this.cardExpirationYear = target.value;
    }
  }

  changePaymentMethod(method: EPaymentMethod): void {
    if (method === EPaymentMethod.CreditCard) {
      this.addCardValidators();
    }
    else {
      this.removeCardValidators();
    }

    this.isCreditCard = method === EPaymentMethod.CreditCard;
  }

  addCardValidators(): void {
    const cardFieldNames = ['cardHolderName', 'cardNumber', 'cardExpirationMonth', 'cardExpirationYear', 'cardSecurityCode'];

    cardFieldNames.forEach(c => {
      const cardHolderName = this.registerForm.get(c) as FormControl;
      cardHolderName.setValidators([Validators.required]);
      cardHolderName.updateValueAndValidity();
    });
  }

  removeCardValidators(): void {
    const cardFieldNames = ['cardHolderName', 'cardNumber', 'cardExpirationMonth', 'cardExpirationYear', 'cardSecurityCode'];

    cardFieldNames.forEach(c => {
      const cardHolderName = this.registerForm.get(c) as FormControl;
      cardHolderName.clearValidators();
      cardHolderName.updateValueAndValidity();
    });
  }

  teste(): void {
    alert(this.state);
  }
}
