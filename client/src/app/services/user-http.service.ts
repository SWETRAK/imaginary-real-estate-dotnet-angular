import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {IUserInfoDto} from "../interfaces/IUserInfoDto";
import {IOfferResultDto} from "../interfaces/IOfferResultDto";
import {IChangePasswordDto} from "../interfaces/IChangePasswordDto";

@Injectable({
  providedIn: 'root'
})
export class UserHttpService {

    constructor(private http: HttpClient) {
    }

    private urlBase: string = "http://localhost:8080/user/"

    public changePassword(newPassword: IChangePasswordDto) {
        return this.http.put<IUserInfoDto>(this.urlBase + "update/password", newPassword, {withCredentials: true});
    }

    public getUserInfo() {
        return this.http.get<IUserInfoDto>(this.urlBase + "info", {withCredentials: true});
    }

    public getLikedOffers() {
        return this.http.get<[IOfferResultDto]>(this.urlBase + "liked", {withCredentials: true});
    }

    public getListedOffers() {
        return this.http.get<[IOfferResultDto]>(this.urlBase + "listed", {withCredentials: true});
    }
}
