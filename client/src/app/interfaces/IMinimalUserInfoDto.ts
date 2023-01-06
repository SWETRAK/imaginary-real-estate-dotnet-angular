export interface IMinimalUserInfoDto {
    firstName: string;
    lastName: string;
    email: string;
}

export const EmptyMinimalUserInfo = (): IMinimalUserInfoDto => {
    return {
        firstName: '',
        lastName: '',
        email: ''
    }
}
