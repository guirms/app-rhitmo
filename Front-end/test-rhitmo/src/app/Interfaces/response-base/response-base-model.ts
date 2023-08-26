export interface ResponseBaseModel<T> {
    sucesso: boolean;
    mensagem: string;
    data: T,
}
