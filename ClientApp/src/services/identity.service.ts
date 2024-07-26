import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../constants'
import { User } from '../models/User';
import { registerUser } from '../contracts/registerUser';
import { loginUser } from '../contracts/loginUser';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  constructor(private http: HttpClient) { }

  registerUser(user : registerUser) : Observable<undefined>{
    return this.http.post<undefined>(API_URL + `users`,user);
  }

  login(user : loginUser) : Observable<User>{
    return this.http.post<User>(API_URL + `users/login`,user)
  }

  isEmailTaken(email:string) : Observable<boolean>{
    return this.http.post<boolean>(API_URL + `users/check-email`,{"Email":email})
  }

  isUserNameTaken(userName: string) : Observable<boolean>{
    return this.http.get<boolean>(API_URL + `users`)
  }
}
