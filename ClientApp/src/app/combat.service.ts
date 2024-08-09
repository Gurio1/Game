import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';
import { Monster } from '../models/Monster';
import { API_URL, JWT_TOKEN } from '../constants'
import { Battle } from '../models/Battle';

@Injectable({
  providedIn: 'root'
})
export class CombatService {

  private hubConnection: signalR.HubConnection;
  monster = new BehaviorSubject<Battle>(new Battle('0',new Monster('0','0','0','0',)));

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(API_URL + 'hub/combat',
      //TO DO: Find a way to pass the JWT token from headers
      {
      withCredentials: true,
      accessTokenFactory: () => {
          return localStorage.getItem(JWT_TOKEN)!;
      },
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets,
    }).withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connected to SignalR hub'))
      .catch(err => console.error('Error connecting to SignalR hub:', err));

    this.hubConnection.on('ReceiveCombatLog', (monster: Battle) => {
      console.log(monster);
      this.monster.next(monster);
    });
  }

  getMonster() : Observable<Battle>{
    return this.monster.asObservable();
  }

  public Attack(){
    this.hubConnection?.invoke("Attack").then(r => console.log(r));
  }
}
