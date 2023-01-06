import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {AuthHttpService} from "../../services/auth-http.service";
import {LocalStorageService} from "../../services/local-storage.service";
import {EmptyUserInfoDto, IUserInfoDto} from "../../interfaces/IUserInfoDto";

@Component({
  selector: 'app-ire-topbar',
  templateUrl: './ire-topbar.component.html',
  styleUrls: ['./ire-topbar.component.css']
})
export class IreTopBarComponent implements OnInit {

    @ViewChild('navBurger', {static:true }) protected navBurger: ElementRef | undefined;
    @ViewChild('navMenu', {static : true }) protected navMenu: ElementRef| undefined;

    protected logged: boolean = false;
    protected userInfo: IUserInfoDto = EmptyUserInfoDto();

    constructor(
        private authHttpService: AuthHttpService,
        private localStorageService: LocalStorageService
    ) {
    }

    ngOnInit() {
        const userFromStorage = this.localStorageService.getUser();
        if(userFromStorage !== null) {
            this.logged = true;
            this.userInfo = userFromStorage;
        }
    }

    protected toggleNavbar = () => {
        if(this.navBurger !== undefined && this.navMenu !== undefined) {
            this.navBurger.nativeElement.classList.toggle('is-active');
            this.navMenu.nativeElement.classList.toggle('is-active');
        }
    }
}
