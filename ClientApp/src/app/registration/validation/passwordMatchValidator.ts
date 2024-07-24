import { Injectable } from "@angular/core";
import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

Injectable({ providedIn: 'root' })
export function passwordMatchValidator() : ValidatorFn{
    return (control: AbstractControl): ValidationErrors | null =>{
        const isMatch = control.get('password')?.value === control.get('confirmPassword')?.value;
        console.log(isMatch);
        return isMatch ? null : { passwordMismatch: true }
    };
}