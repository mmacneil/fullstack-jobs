import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Shell } from '../shell/shell.service';
import { ListComponent } from './list/list.component'; 

const routes: Routes = [
    Shell.childRoutes([
        { path: 'jobs/list', component: ListComponent }               
      ])
    ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class JobsRoutingModule { }