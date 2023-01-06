export interface IUserInfoDto {
    email: string;
    dateOfBirth: Date;
    firstName: string;
    lastName: string;
    token: string;
    role: string;
}

export const EmptyUserInfoDto = (): IUserInfoDto => {
    return {
        email: '',
        dateOfBirth: new Date(),
        firstName: '',
        lastName: '',
        token: '',
        role: ''
    }
}
