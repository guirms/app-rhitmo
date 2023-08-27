export interface DefaultResponse<T> {
    success: boolean;
    message: string;
    objectData: T; 
}