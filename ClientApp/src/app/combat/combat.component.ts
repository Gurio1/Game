import { Component } from '@angular/core';
import { CombatService } from '../combat.service';
import { Monster } from '../../models/Monster';

@Component({
  selector: 'app-combat',
  standalone: true,
  imports: [],
  templateUrl: './combat.component.html',
  styleUrl: './combat.component.css'
})
export class CombatComponent {
  public monster!: Monster;

  constructor(
    public combatService: CombatService
  ) {}

  ngOnInit() {
    this.combatService.getMonster().subscribe((monster) => {
      this.monster = monster;
    });
  }

  async attack(){
    this.combatService.Attack();
  }
}
