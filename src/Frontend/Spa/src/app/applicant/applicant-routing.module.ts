import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Shell } from '../shell/shell.service';
import { ApplicationConfirmationComponent } from './application-confirmation/application-confirmation.component';


const routes: Routes = [
    Shell.childRoutes([
        { path: 'applicant/confirmation', component: ApplicationConfirmationComponent }       
      ])
    ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class ApplicantRoutingModule { }