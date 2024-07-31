import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { API_URL } from '../constants'
import { User } from '../models/User';
import { registerUser } from '../contracts/registerUser';
import { loginUser } from '../contracts/loginUser';
import { JWT_TOKEN } from '../constants';
import { identityTokenResponse } from '../contracts/api-responses/identityTokenResponse';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  constructor(private http: HttpClient) { }

  registerUser(user : registerUser) : Observable<identityTokenResponse>{
    return this.http.post<identityTokenResponse>(API_URL + `users`,user)
    .pipe(
        tap((response: identityTokenResponse) => {
          localStorage.setItem(JWT_TOKEN, response.token);
        }),
        catchError(this.handleError)
      );;
  }

  login(user : loginUser) : Observable<identityTokenResponse>{
    return this.http.post<identityTokenResponse>(API_URL + `users/login`,user)
    .pipe(
        tap((response: identityTokenResponse) => {
          localStorage.setItem(JWT_TOKEN, response.token);
        }),
        catchError(this.handleError)
      );
  }

  isEmailTaken(email:string) : Observable<boolean>{
    return this.http.post<boolean>(API_URL + `users/check-email`,{"Email":email})
  }

  isUserNameTaken(userName: string) : Observable<boolean>{
    return this.http.get<boolean>(API_URL + `users`)
  }

  private handleError(error: HttpErrorResponse) {
  if (error.status === 0) {
    // A client-side or network error occurred. Handle it accordingly.
    console.error('An error occurred:', error.error);
  } else {
    // The backend returned an unsuccessful response code.
    // The response body may contain clues as to what went wrong.
    console.error(
      `Backend returned code ${error.status}, body was: `, error.error);
  }
  // Return an observable with a user-facing error message.
  return throwError(() => new Error('Something bad happened; please try again later.'));
}
}
