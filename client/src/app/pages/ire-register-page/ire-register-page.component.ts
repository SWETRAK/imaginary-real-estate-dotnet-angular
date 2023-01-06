import {Component, OnInit} from '@angular/core';
import {
    AbstractControl,
    FormBuilder,
    FormControl,
    FormGroup,
    ValidationErrors,
    ValidatorFn,
    Validators
} from "@angular/forms";
import {IRegisterUserWithPasswordDto} from "../../interfaces/IRegisterUserWithPasswordDto";
import {AuthHttpService} from "../../services/auth-http.service";
import {IUserInfoDto} from "../../interfaces/IUserInfoDto";
import {Router} from "@angular/router";
import {LocalStorageService} from "../../services/local-storage.service";

@Component({
  selector: 'app-ire-register-page',
  templateUrl: './ire-register-page.component.html',
  styleUrls: ['./ire-register-page.component.css']
})
export class IreRegisterPageComponent  implements OnInit {

    protected registerForm: FormGroup;

    constructor(
        formBuilder: FormBuilder,
        private router: Router,
        private authHttpService: AuthHttpService,
        private localStorageService: LocalStorageService
    ) {
        this.registerForm = formBuilder.group(
            {
                email: new FormControl('', [
                    Validators.required,
                    Validators.email
                ]),
                firstName: new FormControl('', [Validators.required]),
                lastName: new FormControl('', [Validators.required]),
                dateOfBirth: new FormControl('', [
                    Validators.required,
                    this.ageValidators()
                ]),
                password: new FormControl('', [
                    Validators.required,
                    Validators.minLength(8)
                ]),
                repeatPassword: new FormControl('', [
                    Validators.required,
                    Validators.minLength(8),
                ]),
                role: new FormControl('USER')
            },
            {
                validator: this.repeatPasswordValidator
            }
        );
    }

    ngOnInit() {
    }

    protected registerUser = () => {
        if (this.registerForm.valid) {
            const registerUserDto: IRegisterUserWithPasswordDto = this.registerForm.value;

            this.authHttpService
                .registerUser(registerUserDto)
                .subscribe({
                        next: async (userInfo: IUserInfoDto) => {
                            this.localStorageService.setUser(userInfo);
                            await this.router.navigateByUrl("/home");
                        },
                        error: (error: any) => {
                            if (error.error.status === 400) {
                                const errors: Array<any> = error.error.errors;
                                if("Email" in errors) {
                                    this.registerForm.get("email")?.setErrors({emailTaken: true});
                                }

                                if("Password" in errors) {
                                    this.registerForm.get("email")?.setErrors({toShortPassword: true});
                                }

                                if("RepeatPassword" in errors) {
                                    this.registerForm.get("email")?.setErrors({notMatchPassword: true});
                                }

                                if("DateOfBirth" in errors) {
                                    this.registerForm.get("dateOfBirth")?.setErrors({tooYoung: true});
                                }

                                if("FirstName" in errors) {
                                    this.registerForm.get("firstName")?.setErrors({required: true});
                                }

                                if("LastName" in errors) {
                                    this.registerForm.get("lastName")?.setErrors({required: true});
                                }

                                if("Role" in errors) {
                                    this.registerForm.get("role")?.setErrors({required: true});
                                }
                            }
                        }
                    }
                );
        }
    }

    private repeatPasswordValidator = (control: AbstractControl): ValidationErrors | null => {
        const password = control.get("password")
        const repeatPassword = control.get("repeatPassword");
        if (password != null && repeatPassword != null) {
            if (password.value != repeatPassword.value) {
                repeatPassword.setErrors({incorrect: true});
                return {incorrect: {value: control.value}};
            } else {
                return null;
            }
        } else {
            return null;
        }
    }

    private ageValidators = (): ValidatorFn => {
        return (control: AbstractControl): ValidationErrors | null => {
            const dateOfBirth = Date.parse(control.value);
            let timeDiff = Math.abs(Date.now() - dateOfBirth);
            let age = Math.floor((timeDiff / (1000 * 3600 * 24)) / 365.25);
            return age < 18 ? {tooYoung: {value: control.value}} : null;
        };
    }
}
