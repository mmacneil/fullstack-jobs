import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployerRoutingModule } from './employer-routing.module';
import { JobsComponent } from './jobs/jobs.component';
import { SharedModule }   from '../shared/shared.module';



@NgModule({
  declarations: [JobsComponent],
  imports: [
    CommonModule,
    EmployerRoutingModule,
    SharedModule
  ]
})
export class EmployerModule { }
