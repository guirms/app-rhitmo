import { EPaymentMethod } from "../enums/EPaymentMethod";

export interface AddCustomerRequest {
    name: string;
    email: string;
    cpf: string;
    address: string;
    state: string;
    cep: string;
    city: string;
    paymentMethod: EPaymentMethod;
    cardHolderName?: string | null;
    cardNumber?: string | null;
    cardExpirationMonth?: string | null;
    cardExpirationYear?: string | null;
    cardSecurityCode ?: string | null;
}