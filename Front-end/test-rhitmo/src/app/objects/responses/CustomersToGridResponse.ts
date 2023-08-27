import { EPaymentMethod } from "../enums/EPaymentMethod";

export interface DefaultResponse {
    success: boolean;
    message: string;
    data: CustomersToGridResponse[];
}

export interface CustomersToGridResponse {
    customerId: number;
    name: string;
    email: string;
    cpf: string;
    address: string;
    state: string;
    city: string;
    cep: string;
    paymentMethod: EPaymentMethod;
    insertedAt: string;
    creditCardDto?: CreditCardDto | null;
}

export interface CreditCardDto {
    name: string;
    number: string;
    expirationMonth: string;
    expirationYear: string;
    securityCode: string;
}

