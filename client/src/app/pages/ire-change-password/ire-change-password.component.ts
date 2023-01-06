import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {UserHttpService} from "../../services/user-http.service";
import {IChangePasswordDto} from "../../interfaces/IChangePasswordDto";
import {IUserInfoDto} from "../../interfaces/IUserInfoDto";
import {Router} from "@angular/router";

@Component({
  selector: 'app-ire-change-password',
  templateUrl: './ire-change-password.component.html',
  styleUrls: ['./ire-change-password.component.css']
})
export class IreChangePasswordComponent implements OnInit{

    public changePasswordForm : FormGroup

    constructor(
        private formBuilder: FormBuilder,
        private userHttpService: UserHttpService,
        private router: Router
    ) {
        this.changePasswordForm = this.formBuilder.group({
            currentPassword: new FormControl('', [
                Validators.required,
                Validators.minLength(8)]),
            newPassword: new FormControl('', [
                Validators.required,
                Validators.minLength(8)
            ]),
            repeatPassword: new FormControl('', [
                Validators.required,
                Validators.minLength(8)
            ]),
        });
    }

    ngOnInit() {
    }

    protected changePassword = () => {
        if(this.changePasswordForm.valid) {
            const newChangePassword: IChangePasswordDto = this.changePasswordForm.value;
            this.userHttpService.changePassword(newChangePassword).subscribe({
                next: (result: IUserInfoDto) => {
                    this.router.navigateByUrl("/user/info");
                },
                error: (error: any) => {
                    console.log(error);
                }
            })
        }
    }
}
