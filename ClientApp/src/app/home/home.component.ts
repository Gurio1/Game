import { NgFor, NgIf } from '@angular/common';
import { Component } from '@angular/core';

interface Item {
  name: string;
  img: string;
  slot: string;
}

interface EquipmentSlot {
  name: string;
  item: Item | null;
}

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NgIf,NgFor],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  inventory: Item[] = [
    { name: 'Sword', img: '../../assets/avatar.jpeg', slot: 'weapon' },
    { name: 'Shield', img: '../../assets/avatar.jpeg', slot: 'offhand' },
    { name: 'Health Potion', img: '../../assets/avatar.jpeg', slot: 'potion' },
    { name: 'Mana Potion', img: '../../assets/avatar.jpeg', slot: 'potion' },
    { name: 'Gold', img: '../../assets/avatar.jpeg', slot: 'misc' }
  ];

  equipmentSlots: EquipmentSlot[] = [
    { name: 'Weapon', item: null },
    { name: 'Offhand', item: null },
    { name: 'Armor', item: null },
    { name: 'Potion', item: null },
    { name: 'Misc', item: null }
  ];

  equipItem(item: Item) {
    const slot = this.equipmentSlots.find(s => s.name.toLowerCase() === item.slot);
    if (slot) {
      slot.item = item;
      this.inventory = this.inventory.filter(i => i !== item);
    }
  }

  unequipItem(slot: EquipmentSlot) {
    if (slot.item) {
      this.inventory.push(slot.item);
      slot.item = null;
    }
  }
}
