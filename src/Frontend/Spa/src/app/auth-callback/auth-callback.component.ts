import { Component, OnInit } from '@angular/core';
import { AuthService } from '../core/authentication/auth.service';
import { ConfigService } from '../core/services/config.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.scss']
})
export class AuthCallbackComponent implements OnInit {

  constructor(private authService: AuthService, private configService: ConfigService, private router: Router) { }

  async ngOnInit() {
    await this.authService.completeAuthentication();
    this.router.navigateByUrl(this.configService.getDefaultRoute(this.authService.role));    
  }
}