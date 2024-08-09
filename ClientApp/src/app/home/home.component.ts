import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { PlayerService } from '../../services/player.service';
import { Player } from '../../models/Player';
import { RouterLink } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'home',
  standalone: true,
  imports: [RouterLink,AsyncPipe,NgIf],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent{
  player$!: Observable<Player>;

  constructor(private playerService : PlayerService){}

  ngOnInit(): void {
    this.player$ = this.playerService.getPlayer()
  }
}
