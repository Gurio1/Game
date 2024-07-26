import { Component, SimpleChanges } from '@angular/core';
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
  healthPercentage: number = 100;

  constructor(
    public combatService: CombatService
  ) {}

  ngOnInit() {
    this.combatService.getMonster().subscribe((monster) => {
      this.monster = monster;
      this.updateHealthPercentage();
    });
  }

  async attack(){
    this.combatService.Attack();
  }

  updateHealthPercentage(): void {
    this.healthPercentage = (Number(this.monster.currentHp) / Number(this.monster.maxHp)) * 100;
  }
}
