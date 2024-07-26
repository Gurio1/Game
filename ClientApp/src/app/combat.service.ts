import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';
import { Monster } from '../models/Monster';
import { API_URL } from '../constants'

@Injectable({
  providedIn: 'root'
})
export class CombatService {

  private hubConnection: signalR.HubConnection;
  monster = new BehaviorSubject<Monster>(new Monster('0','0','0','0'));

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(API_URL + 'combatHub').withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connected to SignalR hub'))
      .catch(err => console.error('Error connecting to SignalR hub:', err));

    this.hubConnection.on('ReceiveCombatLog', (monster: Monster) => {
      console.log(monster);
      this.monster.next(monster);
    });
  }

  getMonster() : Observable<Monster>{
    return this.monster.asObservable();
  }

  public Attack(){
    this.hubConnection?.invoke("Attack","test").then(r => console.log(r));
  }
}
