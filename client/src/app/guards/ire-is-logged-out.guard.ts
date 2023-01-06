import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { catchError, map, Observable, of } from 'rxjs';
import { AuthHttpService } from "../services/auth-http.service";
import { IUserInfoDto } from "../interfaces/IUserInfoDto";
import { LocalStorageService } from "../services/local-storage.service";

@Injectable({
  providedIn: 'root'
})
export class IreIsLoggedOutGuard implements CanActivate {

    constructor(
        private authHttpService: AuthHttpService,
        private localStorageService: LocalStorageService
    ) {
    }

    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        return this.authHttpService.isUserLoggedIn().pipe(
            map((user: IUserInfoDto) => {
                this.localStorageService.setUser(user);
                return false;
            }),
            catchError((error: any) => {
                this.localStorageService.removeUser()
                return of(true);
            })
        );
    }
}
