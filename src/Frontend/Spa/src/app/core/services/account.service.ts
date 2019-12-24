import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { AccountSignup } from '../../core/models/account-signup';

import { BaseService } from "./base.service";
import { ConfigService } from './config.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService extends BaseService  {

  constructor(private http: HttpClient, private configService: ConfigService) { 
    super();   
  }

  signup(accountSignup: AccountSignup) {    
    return this.http.post(this.configService.authApiURI + '/accounts', accountSignup).pipe(catchError(this.handleError));
  }
}