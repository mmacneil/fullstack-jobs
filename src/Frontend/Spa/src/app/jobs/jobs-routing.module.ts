import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Shell } from '../shell/shell.service';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component'; 

const routes: Routes = [
    Shell.childRoutes([
        { path: 'jobs/list', component: ListComponent },
        { path: 'jobs/:id/details', component: DetailsComponent }               
      ])
    ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class JobsRoutingModule { }