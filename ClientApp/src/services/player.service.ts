import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { createPlayer } from '../contracts/createPlayer';
import { API_URL, JWT_TOKEN } from '../constants';
import { Player } from '../models/Player';

let httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  }),
};

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  constructor(private http: HttpClient) { }

  createPlayer(playerInfo : createPlayer) {
    return this.http.post(API_URL + 'players', playerInfo,this.createAuthHeaders());
  }

  getPlayer() : Observable<Player> {
    return this.http.get<Player>(API_URL + 'players',this.createAuthHeaders());
  }

  isUserNameTaken(userName : string) : Observable<boolean>{
    return this.http.post<boolean>(API_URL + 'players/check-userName',{userName:userName},this.createAuthHeaders());
  }

  createAuthHeaders() {
    let jwtToken = localStorage.getItem(JWT_TOKEN);
    let httpOptions = {
          headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization' : `Bearer ${jwtToken}`
      }),
    };

    return httpOptions;
  }
}
