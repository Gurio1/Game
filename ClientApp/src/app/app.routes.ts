import { Routes } from '@angular/router';
import { LoginComponent } from './identity/login/login.component';
import { RegistrationComponent } from './identity/registration/registration.component';
import { CreateCharacterComponent } from './create-character/create-character.component';
import { LayoutComponent } from './layout/layout.component';
import { HomeComponent } from './home/home.component';
import { CombatComponent } from './combat/combat.component';

export const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegistrationComponent }
    ]
  },
  {path: 'create-character',component: CreateCharacterComponent},
  {path: "home",component: HomeComponent},
  {path: "combat",component: CombatComponent}
];
