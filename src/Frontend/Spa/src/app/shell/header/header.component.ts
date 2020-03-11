import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from '../../core/authentication/auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {

  name: string;
  isAuthenticated: boolean;
  subscription: Subscription;

  constructor(public authService: AuthService) { }

  ngOnInit() {
    this.subscription = this.authService.authNavStatus$.subscribe(status => this.isAuthenticated = status);
    this.name = this.authService.name;
  }

  showAccessToken() {

    // remove any existing entries
    document.querySelectorAll('.access-token').forEach(function (a) {
      a.remove()
    });

    var token = document.createElement("p");
    token.className = "access-token";
    token.innerText =  this.authService.authorizationHeaderValue;
    document.getElementsByClassName("container")[0].insertAdjacentElement('afterbegin', token);    
  }

  async signout() {
    await this.authService.signout();
  }

  ngOnDestroy() {
    // prevent memory leak when component is destroyed
    this.subscription.unsubscribe();
  }
}