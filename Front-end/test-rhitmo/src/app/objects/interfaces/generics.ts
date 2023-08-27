import { EPaymentMethod } from "../enums/EPaymentMethod";
import { CreditCardDto } from "../responses/CustomersToGridResponse";

export interface NoContentResponse {
    success: boolean;
    message: string;
}

export interface BrazilLocations {
    States: Locations[];
}

interface Locations {
    Acronym : string;
    Name: string;
    Cities: string[];
}

export interface CustomerSharedData {
    isEditing: boolean,
    name: string;
    email: string;
    cpf: string;
    address?: string;
    state?: string;
    city?: string;
    cep?: string;
    paymentMethod?: EPaymentMethod;
    insertedAt?: string;
    creditCardDto?: CreditCardDto | null;
}
