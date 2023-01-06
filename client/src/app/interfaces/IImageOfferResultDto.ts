export interface IImageOfferResultDto {
    identifier: string;
    frontPhoto: boolean;
}

export const EmptyImageOfferResultDto = (): IImageOfferResultDto => {
    return {
        identifier: "",
        frontPhoto: false
    }
}
