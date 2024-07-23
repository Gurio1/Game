import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { registerUser } from '../contracts/registerUser';
import { Observable } from 'rxjs';
import { API_URL } from '../constants'
import { loginUser } from '../contracts/LoginUser';
import { user } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  constructor(private http: HttpClient) { }

  registerUser(user : registerUser) : Observable<undefined>{
    return this.http.post<undefined>(API_URL + `users`,user);
  }

  login(user : loginUser) : Observable<user>{
    return this.http.post<user>(API_URL + `users/login`,user)
  }
}
