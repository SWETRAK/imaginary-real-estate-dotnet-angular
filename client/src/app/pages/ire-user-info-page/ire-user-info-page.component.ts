import {Component, OnInit} from '@angular/core';
import {EmptyUserInfoDto, IUserInfoDto} from "../../interfaces/IUserInfoDto";
import {LocalStorageService} from "../../services/local-storage.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-ire-user-info-page',
  templateUrl: './ire-user-info-page.component.html',
  styleUrls: ['./ire-user-info-page.component.css']
})
export class IreUserInfoPageComponent implements OnInit{

    protected user: IUserInfoDto = EmptyUserInfoDto();

    constructor(
        private localStorageService: LocalStorageService,
        private router: Router
    ) {
        const user = this.localStorageService.getUser();
        if(user === null) {
            this.router.navigateByUrl("/home");
        } else {
            this.user = user;
        }
    }

    ngOnInit() {
    }


}
