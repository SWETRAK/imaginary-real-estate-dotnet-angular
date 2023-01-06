import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {AuthHttpService} from "../../services/auth-http.service";
import {ILoginUserWithPasswordDto} from "../../interfaces/ILoginUserWithPasswordDto";
import {IUserInfoDto} from "../../interfaces/IUserInfoDto";
import {LocalStorageService} from "../../services/local-storage.service";

@Component({
  selector: 'app-ire-login-page',
  templateUrl: './ire-login-page.component.html',
  styleUrls: ['./ire-login-page.component.css']
})
export class IreLoginPageComponent implements OnInit{

    protected loginForm: FormGroup

    protected loginError: boolean = false;

    constructor(
        formBuilder: FormBuilder,
        private router: Router,
        private authHttpService: AuthHttpService,
        private localStorageService: LocalStorageService
    ) {
        this.loginForm = formBuilder.group({
            email: new FormControl("", [
                Validators.email,
                Validators.required
            ]),
            password: new FormControl("", [
                Validators.required,
                Validators.minLength(8)
            ])
        });
    }

    async ngOnInit () {
    }

    protected loginUser() {
        if (this.loginForm.valid) {
            const loginUserDto: ILoginUserWithPasswordDto = this.loginForm.value;
            this.authHttpService.loginUser(loginUserDto).subscribe({
                next: async (userInfo: IUserInfoDto) => {
                    this.localStorageService.setUser(userInfo);
                    await this.router.navigateByUrl("/home");
                },
                error: (error: any) => {
                    if (error.error.status === 400) {
                        const errors: Array<any> = error.error.errors;
                        if("Email" in errors) {
                            this.loginForm.get("email")?.setErrors({incorrect: true});
                        }

                        if("Password" in errors) {
                            this.loginForm.controls["password"].setErrors({incorrect: true});
                        }
                    }

                    if(error.status === 401) {
                        this.loginError = true;
                    }
                }
            });
        }

    }
}
