import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { finalize } from 'rxjs/operators'
import { AccountSignup } from '../../core/models/account-signup';
import { AccountService } from '../../core/services/account.service';
import { AuthService } from '../../core/authentication/auth.service';

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

  constructor(private accountService: AccountService, private authService: AuthService) { }

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

  public signup = (signupFormValue: any) => {

    if (this.signupForm.valid) {

      this.busy=true;
    
      let accountSignup: AccountSignup = {
        fullName: signupFormValue.fullName,
        email: signupFormValue.email,
        password: signupFormValue.password,
        role: signupFormValue.role     
      }
  
      this.accountService.signup(accountSignup)
      .pipe(finalize(() => {
        this.busy=false;
      }))  
      .subscribe(
      result => {         
         if(result) {
           // redirect to login            
           this.authService.login(true, accountSignup.email);
         }
      },
      error => {
        this.error = error;       
      });
    }
  } 
}