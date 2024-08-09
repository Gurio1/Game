import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { uniqueUserNameValidator } from './validators/uniqueUserNameValidator';
import { createPlayer } from '../../contracts/createPlayer';
import { PlayerService } from '../../services/player.service';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { NgIf } from '@angular/common';

@Component({
  selector: 'create-character',
  standalone: true,
  imports: [RouterLink,ReactiveFormsModule,NgIf,MatProgressBarModule],
  templateUrl: './create-character.component.html',
  styleUrl: './create-character.component.css'
})
export class CreateCharacterComponent {
  router: any;

  constructor(private uniqueUserNameValidator : uniqueUserNameValidator,private playerService : PlayerService){}

  createForm = new FormGroup({
    userName: new FormControl('',{
      asyncValidators:[
        this.uniqueUserNameValidator.validate.bind(this.uniqueUserNameValidator)
      ],
      validators:[
        Validators.required,
        Validators.pattern(/^[a-zA-Z0-9]*$/),
        Validators.minLength(4)
      ],
      updateOn: 'blur'
    })
  })

  submitForm() {
      if(this.createForm.valid){

        console.log(this.createForm.controls.userName.value!);

        let player = new createPlayer(
          this.createForm.controls.userName.value!
        );

        var result = this.playerService.createPlayer(player);

        result.subscribe({
          next: value => this.router.navigate(['/home']),
          error: err => console.error('Observable emitted an error: ' + err)
        })
      }
  }
}
