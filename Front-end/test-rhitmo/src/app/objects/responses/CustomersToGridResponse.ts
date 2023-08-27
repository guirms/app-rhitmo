export interface DefaultResponse {
    success: boolean;
    message: string;
    data: CustomersToGridResponse[]; 
}
export interface CustomersToGridResponse {
    name: string;
    email: string;
    cpf: string;
    insertedAt: string;
}