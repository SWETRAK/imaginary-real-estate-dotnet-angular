import {EmptyMinimalUserInfo, IMinimalUserInfoDto} from "./IMinimalUserInfoDto";
import {IImageOfferResultDto} from "./IImageOfferResultDto";

export interface IOfferResultDto {
    identifier: string;
    title: string;
    description: string;
    address: string;
    author: IMinimalUserInfoDto;
    price: number;
    bedrooms: number;
    bathrooms: number;
    area: number;
    likes: number;
    images: Array<IImageOfferResultDto>;
}

export const EmptyOfferResult = () : IOfferResultDto => {
    return {
        identifier: '',
        title: '',
        description: '',
        address: '',
        author: EmptyMinimalUserInfo(),
        price: 0,
        bedrooms: 0,
        bathrooms: 0,
        area: 0,
        likes: 0,
        images: [],
    }
}
