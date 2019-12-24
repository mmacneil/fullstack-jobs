import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { finalize } from 'rxjs/operators'


@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

  signupForm: FormGroup; 
  error: string; 
  hide = true;
  busy = false;

  constructor() { }

  public signup = (signupFormValue: any) => {

  }

  ngOnInit() {
    this.signupForm = new FormGroup({
      fullName: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(25), Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@!#$%^&+=]).*$')]),      
      role: new FormControl('', [Validators.required])
    });
  }

  public hasError = (controlName: string, errorName: string) =>{
    return this.signupForm.controls[controlName].hasError(errorName);
  }
}