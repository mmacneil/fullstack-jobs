import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SignupComponent } from './signup/signup.component';
import { Shell } from './../shell/shell.service';

const routes: Routes = [
Shell.childRoutes([
    { path: 'signup', component: SignupComponent }   
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class AccountRoutingModule { }