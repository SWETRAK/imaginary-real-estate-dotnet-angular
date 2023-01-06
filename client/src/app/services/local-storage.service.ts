import { Injectable } from '@angular/core';
import { IUserInfoDto } from "../interfaces/IUserInfoDto";

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

    constructor() {
    }

    private static USERKEY = "loggedUser"

    public setUser = (userInfo: IUserInfoDto) => {
        const userInfoJson = JSON.stringify(userInfo);
        localStorage.setItem(LocalStorageService.USERKEY, userInfoJson);
    }

    public getUser = (): IUserInfoDto | null => {
        const userInfoJson = localStorage.getItem(LocalStorageService.USERKEY);
        if (userInfoJson == undefined) return null;
        else return JSON.parse(userInfoJson);
    }

    public removeUser = () => {
        localStorage.removeItem(LocalStorageService.USERKEY);
    }
}
