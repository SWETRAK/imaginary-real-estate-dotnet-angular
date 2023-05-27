import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";

import { ILoginUserWithPasswordDto } from '../interfaces/ILoginUserWithPasswordDto';
import { IRegisterUserWithPasswordDto } from "../interfaces/IRegisterUserWithPasswordDto";
import { IUserInfoDto } from '../interfaces/IUserInfoDto';

@Injectable({
  providedIn: 'root'
})
export class AuthHttpService {

    constructor(private http: HttpClient) {
    }

    private urlBase: string = "http://localhost:8080/auth";

    public isUserLoggedIn = () => {
        return this.http.get<IUserInfoDto>(this.urlBase, {withCredentials: true});
    }

    public loginUser = (loginUserWithPasswordDto: ILoginUserWithPasswordDto) => {
        return this.http.post<IUserInfoDto>(this.urlBase + "/login", loginUserWithPasswordDto, {withCredentials: true});
    }

    public registerUser = (registerUserWithPasswordDto: IRegisterUserWithPasswordDto) => {
        return this.http.post<IUserInfoDto>(this.urlBase + "/register", registerUserWithPasswordDto, {withCredentials: true});
    }

    public logoutUser = () => {
        return this.http.delete<boolean>(this.urlBase + "/logout", {withCredentials: true});
    }
}
