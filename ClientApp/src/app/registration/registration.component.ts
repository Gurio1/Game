import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FormGroup,FormControl, ReactiveFormsModule } from '@angular/forms';
import { IdentityService } from '../../services/identity.service';
import { registerUser } from '../../contracts/registerUser';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [RouterLink,ReactiveFormsModule],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {

constructor(private identityService : IdentityService){}

  registerForm = new FormGroup({
    userName: new FormControl(),
    email: new FormControl(),
    password: new FormControl(),
    confirmPassword: new FormControl()
  });

  submitForm() {
    let user = new registerUser(
      this.registerForm.controls.userName.value,
      this.registerForm.controls.email.value,
      this.registerForm.controls.password.value,
      this.registerForm.controls.confirmPassword.value,
    )

    var result = this.identityService.registerUser(user);

    console.log(user);

    result.subscribe(val =>{
      console.log(val);
    });
  }
}
