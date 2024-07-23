import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { registerUser } from '../../contracts/registerUser';
import { IdentityService } from '../../services/identity.service';
import { loginUser } from '../../contracts/LoginUser';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink,ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

constructor(private identityService: IdentityService){}

  loginForm = new FormGroup({
    userName: new FormControl(),
    email: new FormControl(),
    password: new FormControl(),
    confirmPassword: new FormControl()
  });

  submitForm() {
    let user = new loginUser(
      this.loginForm.controls.email.value,
      this.loginForm.controls.password.value,
    )

    var result = this.identityService.login(user);

    console.log(user);

    result.subscribe(val =>{
      console.log(val);
    });
  }
}
