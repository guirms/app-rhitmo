import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { EPaymentMethod } from 'src/app/objects/enums/EPaymentMethod';
import { KeyAndValue } from 'src/app/objects/interfaces/generics';
import { AddCustomerRequest } from 'src/app/objects/requests/AddCustomerRequest ';
import { BaseService } from 'src/app/services/base/base.service';
import { CustomerService } from 'src/app/services/customer/customer.service';
import { SharedDataService } from 'src/app/services/shared-data/shared-data.service';
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
  cardHolderName!: string | null;
  cardNumber!: string | null;
  cardSecurityCode!: string | null;
  cardExpirationMonth!: string | null;
  cardExpirationYear!: string | null;
  cityList: KeyAndValue[] = [];
  customerId!: number;

  statePosition = 0;
  cityPosition = 0;

  readonly stateList: KeyAndValue[] = [];
  readonly monthList: KeyAndValue[] = [];
  readonly yearList: KeyAndValue[] = [];

  constructor(private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    private baseService: BaseService,
    public customerService: CustomerService,
    private sharedDataService: SharedDataService) {

    this.setFields();
  }
  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      cpf: ['', [Validators.required]],
      address: ['', [Validators.required]],
      state: [this.statePosition, [Validators.required]],
      cep: ['', [Validators.required]],
      city: [this.cityPosition, [Validators.required]],
      cardHolderName: ['', [Validators.required]],
      cardExpirationMonth: [0, [Validators.required]],
      cardExpirationYear: [0, [Validators.required]],
      cardNumber: ['', [Validators.required]],
      cardSecurityCode: ['', [Validators.required]]
    });
  }

  submitClient(): void {
    if (!this.isFormValid()) {
      this.toastrService.warning('Há campos digitados incorretamente ou vazios');
      return;
    }

    const addCustomerRequest: AddCustomerRequest = {
      name: this.name,
      email: this.email,
      cpf: this.removeSpacesAndSpecialChars(this.cpf),
      address: this.address,
      state: this.stateList.filter(s => s.key === Number(this.state))[0].value ?? '',
      cep: this.removeSpacesAndSpecialChars(this.cep),
      city: this.cityList.filter(c => c.key === Number(this.city))[0].value ?? '',
      paymentMethod: this.isCreditCard ? 1 : 2,
    }

    if (this.isCreditCard) {
      addCustomerRequest.cardHolderName = this.cardHolderName,
        addCustomerRequest.cardNumber = this.removeSpacesAndSpecialChars(this.cardNumber ?? ''),
        addCustomerRequest.cardExpirationMonth = ('0' + this.cardExpirationMonth).slice(-2),
        addCustomerRequest.cardExpirationYear = this.yearList[Number(this.cardExpirationYear) - 1].value,
        addCustomerRequest.cardSecurityCode = this.cardSecurityCode
    }

    if (this.customerId) {
      this.customerService.updateCustomer(addCustomerRequest, this.customerId)
        .subscribe({
          next: (result) => {
            if (result.success) {
              this.toastrService.success(result.message);
              this.baseService.navigate('');
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
    else {
      this.customerService.saveCustomer(addCustomerRequest)
        .subscribe({
          next: (result) => {
            if (result.success) {
              this.toastrService.success(result.message);
              this.baseService.navigate('');
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
  }

  removeSpacesAndSpecialChars(input: string): string {
    return input.replace(/[^a-zA-Z0-9]/g, '');
  }

  validateField(fieldName: string): boolean {
    const formField = this.registerForm.get(fieldName);
    return (formField?.invalid && (formField?.dirty || formField?.touched) && !formField?.errors?.email) ?? false;
  }

  validateEmail(): boolean {
    const formField = this.registerForm.get('email');

    return (formField?.errors?.email && (formField?.dirty || formField?.touched)) ?? false;
  }

  validateCpf(isSubmitted?: boolean): boolean {
    const formField = this.registerForm.get('cpf');

    if (!this.cpf && isSubmitted) {
      return true;
    }

    if (formField) {
      return (this.cpf?.length !== 14 && formField?.touched) ?? false;
    }

    return false;
  }

  validateCep(isSubmitted?: boolean): boolean {
    const formField = this.registerForm.get('cep');

    if (!this.cep && isSubmitted) {
      return true;
    }

    if (formField) {
      return (this.cep?.length !== 12 && formField?.touched) ?? false;
    }

    return false;
  }

  validateCardNumber(isSubmitted?: boolean): boolean {
    const formField = this.registerForm.get('cardNumber');

    if (!this.cardNumber && isSubmitted) {
      return true;
    }

    if (formField) {
      return (this.cardNumber?.length !== 19 && formField?.touched) ?? false;
    }

    return false;
  }

  validateCardSecurityCode(isSubmitted?: boolean): boolean {
    const formField = this.registerForm.get('cardSecurityCode');

    if (!this.cardSecurityCode && isSubmitted) {
      return true;
    }

    if (formField) {
      return (this.cardSecurityCode?.length !== 3 && formField?.touched) ?? false;
    }

    return false;
  }

  validateState(isSubmitted?: boolean): boolean {
    const formField = this.registerForm.get('state');

    if (!this.state && isSubmitted) {
      return true;
    }


    return (!this.state && formField?.touched) ?? false;
  }

  validateCity(isSubmitted?: boolean): boolean {
    const formField = this.registerForm.get('city');

    if (!this.city && isSubmitted) {
      return true;
    }

    return (!this.city && formField?.touched) ?? false;
  }

  validateMonth(isSubmitted?: boolean): boolean {
    const formField = this.registerForm.get('cardExpirationMonth');

    if (!this.cardExpirationMonth && isSubmitted) {
      return true;
    }

    return (!this.cardExpirationMonth && formField?.touched) ?? false;
  }

  validateYear(isSubmitted?: boolean): boolean {
    const formField = this.registerForm.get('cardExpirationYear');

    if (!this.cardExpirationYear && isSubmitted) {
      return true;
    }

    return (!this.cardExpirationYear && formField?.touched) ?? false;
  }

  formatCpf(): void {
    if (this.cpf) {
      this.cpf = this.customerService.formatCpf(this.cpf);
    }
  }

  formatCep(): void {
    if (this.cep) {
      const cleanCep = this.cep.replace(/\D/g, '');

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

      const filteredState = environment.brazilLocations.States
        .filter(s => s.Name === this.stateList[Number(this.state) - 1].value);

      if (this.city) {
        const cityExists = filteredState
          .some(c => c.Cities.some(c => c === this.cityList[Number(this.city) - 1].value));

        if (!cityExists) {
          this.city = null;
        }
      }

      this.cityList = [];

      const cityNames: string[] = filteredState
        .flatMap(state => state.Cities)
        .sort();

      for (let i = 0; i < cityNames.length; i++) {
        this.cityList.push({
          key: i + 1,
          value: cityNames[i]
        });
      }
    }
  }

  clearCity(): void {
    this.cityList = [];
    this.city = null;

    const cityNames: string[] = environment.brazilLocations.States
      .flatMap(state => state.Cities)
      .sort();

    for (let i = 0; i < cityNames.length; i++) {
      this.cityList.push({
        key: i + 1,
        value: cityNames[i]
      });
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

  private addCardValidators(): void {
    const cardFieldNames = ['cardHolderName', 'cardNumber', 'cardExpirationMonth', 'cardExpirationYear', 'cardSecurityCode'];

    cardFieldNames.forEach(c => {
      const cardHolderName = this.registerForm.get(c) as FormControl;
      cardHolderName.setValidators([Validators.required]);
      cardHolderName.updateValueAndValidity();
    });
  }

  private removeCardValidators(): void {
    const cardFieldNames = ['cardHolderName', 'cardNumber', 'cardExpirationMonth', 'cardExpirationYear', 'cardSecurityCode'];

    cardFieldNames.forEach(c => {
      const cardHolderName = this.registerForm.get(c) as FormControl;
      cardHolderName.clearValidators();
      cardHolderName.updateValueAndValidity();
    });
  }

  private isFormValid(): boolean {
    if (!this.customerId && !this.registerForm.invalid) {
      return false;
    }

    if (this.isCreditCard) {
      return !this.validateCardNumber(true) && !this.validateCardSecurityCode(true) && !this.validateCity(true)
        && !this.validateCpf(true) && !this.validateEmail() && !this.validateMonth(true) && !this.validateState(true) && !this.validateYear(true);
    }

    return !this.validateCity(true)
      && !this.validateCpf(true) && !this.validateEmail() && !this.validateState(true);
  }

  private setFields(): void {
    const sharedData = this.sharedDataService.getData();

    const stateNames: string[] = environment.brazilLocations.States.map(s => s.Name)
      .sort();

    for (let i = 0; i < stateNames.length; i++) {
      this.stateList.push({
        key: i + 1,
        value: stateNames[i]
      });
    }

    this.name = sharedData.name;
    this.email = sharedData.email;
    this.cpf = sharedData.cpf;
    
    if (!sharedData || !sharedData.customerId) {
      const cityNames: string[] = environment.brazilLocations.States
        .flatMap(state => state.Cities)
        .sort();

      for (let i = 0; i < cityNames.length; i++) {
        this.cityList.push({
          key: i + 1,
          value: cityNames[i]
        });
      }

      return;
    }

    this.customerId = sharedData.customerId

    if (sharedData.address && sharedData.cep && sharedData.state && sharedData.city && sharedData.paymentMethod) {
      if (sharedData.customerId) {
        const filteredState = environment.brazilLocations.States
          .filter(s => s.Name === sharedData.state);

        this.statePosition = this.stateList.filter(s => s.value === sharedData.state)[0].key;

        const cityNames: string[] = filteredState
          .flatMap(s => s.Cities)
          .sort();

        for (let i = 0; i < cityNames.length; i++) {
          this.cityList.push({
            key: i + 1,
            value: cityNames[i]
          });
        }

        this.cityPosition = this.cityList.filter(s => s.value === sharedData.city)[0].key;
      }

      const monthNames: string[] = environment.months;

      for (let i = 0; i < monthNames.length; i++) {
        this.monthList.push({
          key: i + 1,
          value: monthNames[i]
        });
      }

      const currentYear = new Date().getFullYear();
      const futureYears = 20;
      const yearNames = Array.from({ length: futureYears }, (_, index) => (currentYear + index).toString()).sort();

      for (let i = 0; i < yearNames.length; i++) {
        this.yearList.push({
          key: i + 1,
          value: yearNames[i]
        });
      }

      this.address = sharedData.address;
      this.cep = sharedData.cep;
      this.state = this.stateList.filter(s => s.value === sharedData.state)[0].key.toString();
      this.city = this.cityList.filter(c => c.value === sharedData.city)[0].key.toString();
      this.isCreditCard = sharedData.paymentMethod === EPaymentMethod.CreditCard;

      if (this.isCreditCard && sharedData.creditCardDto) {
        this.cardHolderName = sharedData.creditCardDto.name;
        this.cardNumber = sharedData.creditCardDto.number;
        this.cardExpirationMonth = sharedData.creditCardDto.expirationMonth;
        this.cardExpirationYear = sharedData.creditCardDto.expirationYear;
        this.cardSecurityCode = sharedData.creditCardDto.securityCode;
      }
    }
  }

}
