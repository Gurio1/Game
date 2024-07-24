import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FormGroup,FormControl, ReactiveFormsModule,Validators, NgModel } from '@angular/forms';
import { IdentityService } from '../../services/identity.service';
import { registerUser } from '../../contracts/registerUser';
import { UniqueEmailValidator } from './validation/uniqueEmailValidator';
import { passwordMatchValidator } from './validation/passwordMatchValidator';
import { NgIf } from '@angular/common';
import {MatProgressBarModule} from '@angular/material/progress-bar';
@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [RouterLink,ReactiveFormsModule,NgIf,MatProgressBarModule],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {
  email: string = '';

constructor(private identityService : IdentityService,private uniqueEmailValidator : UniqueEmailValidator,){}

  registerForm = new FormGroup({
    userName: new FormControl('',Validators.pattern(/^[a-zA-Z0-9]*$/)),
    email: new FormControl('',{
      asyncValidators:[
        this.uniqueEmailValidator.validate.bind(this.uniqueEmailValidator)
      ],
      validators:[
        Validators.required,
        Validators.email
      ],
      updateOn: 'blur'
    }),
    password: new FormControl('',[Validators.required,Validators.minLength(8)]),
    confirmPassword: new FormControl('',Validators.required)
  },{ validators: passwordMatchValidator() });

  submitForm() {
      if(this.registerForm.valid){
        let formControls = this.registerForm.controls;

        let user = new registerUser(
        formControls.userName.value!,
        formControls.email.value!,
        formControls.password.value!,
        formControls.confirmPassword.value!
       )

        var result = this.identityService.registerUser(user);

        console.log(user);

        result.subscribe(val =>{
        console.log(val);
        });
      }
  }
}
