export interface LocationResponse {
    success: boolean;
    data: LocationByCepResponse;
}

export interface LocationByCepResponse {
    state: string;
    city: string;
}