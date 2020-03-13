import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Shell } from '../shell/shell.service';
import { JobsComponent } from './jobs/jobs.component';
import { AuthGuard } from '../core/authentication/auth.guard';

const routes: Routes = [
    Shell.childRoutes([
        { path: 'employer/jobs', component: JobsComponent, canActivate: [AuthGuard] }       
      ])
    ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class EmployerRoutingModule { }