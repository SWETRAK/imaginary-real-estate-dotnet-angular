import {Component, OnInit} from '@angular/core';
import {EmptyUserInfoDto, IUserInfoDto} from "../../interfaces/IUserInfoDto";
import {AuthHttpService} from "../../services/auth-http.service";
import {LocalStorageService} from "../../services/local-storage.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-ire-logout-page',
  templateUrl: './ire-logout-page.component.html',
  styleUrls: ['./ire-logout-page.component.css']
})
export class IreLogoutPageComponent implements OnInit{

    protected user: IUserInfoDto = EmptyUserInfoDto();

    constructor(
        private authHttpService: AuthHttpService,
        private localStorageService: LocalStorageService,
        private router: Router
    ) {
        this.user.firstName = "Kamil";
        this.user.lastName = "Pietrak";
    }

    async ngOnInit() {

    }

    protected logout = () => {
        this.authHttpService.logoutUser().subscribe({
            next: async (result: boolean) => {
                if(result) {
                    this.localStorageService.removeUser();
                    await this.router.navigateByUrl("/home");
                }
            },
            error: async (error: any) => {
                if(error.status === 401) {
                    this.localStorageService.removeUser();
                    await this.router.navigateByUrl("/home");
                }
            }
        });
    }

}
