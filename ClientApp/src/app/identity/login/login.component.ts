import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { IdentityService } from '../../../services/identity.service';
import { loginUser } from '../../../contracts/loginUser';
import { passwordValidator } from '../shared/validators/passwordValidator';
import { NgIf } from '@angular/common';



@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink,ReactiveFormsModule,NgIf],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

constructor(private identityService: IdentityService){}

  loginForm = new FormGroup({
    email: new FormControl('',[Validators.required,Validators.email]),
    password: new FormControl('',[Validators.required,Validators.minLength(8),passwordValidator()]),
  });

  submitForm() {

    if(this.loginForm.valid){
      let formControls = this.loginForm.controls;
      console.log("valid")
      let user = new loginUser(
        this.loginForm.controls.email.value!,
        this.loginForm.controls.password.value!,
      )

      var result = this.identityService.login(user);

      console.log(user);

      result.subscribe(val =>{
        console.log(val);
      });
    }
  }
}
