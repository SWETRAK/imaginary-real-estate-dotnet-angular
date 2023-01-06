export interface INewOfferIncomingDto {
    title: string;
    address: string;
    price: number;
    bedrooms: number;
    bathrooms: number;
    area: number;
    description: string;
}

export const EmptyNewOfferIncomingDto = (): INewOfferIncomingDto => {
    return {
        title: '',
        address: '',
        price: 0,
        bedrooms: 0,
        bathrooms: 0,
        area: 0,
        description: '',
    }
}
