import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SignupComponent } from './signup/signup.component';
import { LoginComponent } from './login/login.component';
import { Shell } from './../shell/shell.service';

const routes: Routes = [
Shell.childRoutes([
    { path: 'signup', component: SignupComponent },
    { path: 'login', component: LoginComponent }     
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class AccountRoutingModule { }