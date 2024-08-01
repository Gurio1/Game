import { Injectable } from "@angular/core";
import { AsyncValidator, AbstractControl, ValidationErrors } from "@angular/forms";
import { Observable, map, catchError, of } from "rxjs";
import { PlayerService } from "../../../services/player.service";

@Injectable({ providedIn: 'root' })
export class uniqueUserNameValidator implements AsyncValidator {
  constructor(private identityService: PlayerService) {}

  validate(control: AbstractControl): Observable<ValidationErrors | null> {
    return this.identityService.isUserNameTaken(control.value).pipe(
      map((isUnique) => (isUnique ? { uniqueUserName: true } :null )),

      //TO DO: Handle error
      catchError(() => of({ uniqueEmail: true })),
    );
  }
}