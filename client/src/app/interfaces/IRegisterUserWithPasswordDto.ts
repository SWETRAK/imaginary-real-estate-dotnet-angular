export interface IRegisterUserWithPasswordDto {
    email: string;
    password: string;
    repeatPassword: string;
    dateOfBirth: Date;
    firstName: string;
    lastName: string;
    role: string;
}
