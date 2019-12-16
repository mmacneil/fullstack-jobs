import { Component } from '@angular/core';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {

  name: string;
  isAuthenticated: boolean;
  subscription:Subscription;

  constructor() { } 
}
