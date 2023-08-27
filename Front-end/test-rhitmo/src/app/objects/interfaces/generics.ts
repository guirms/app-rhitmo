export interface BrazilLocations {
    States: Locations[];
}

interface Locations {
    Acronym : string;
    Name: string;
    Cities: string[];
}

export interface KeyAndValue{
    key: number;
    value: string;
}
