import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CombatComponent } from "./combat/combat.component";
import { RegistrationComponent } from "./registration/registration.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CombatComponent, RegistrationComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ClientApp';
}
