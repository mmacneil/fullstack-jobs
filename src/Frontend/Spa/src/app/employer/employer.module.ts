import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployerRoutingModule } from './employer-routing.module';
import { JobsComponent } from './jobs/jobs.component';



@NgModule({
  declarations: [JobsComponent],
  imports: [
    CommonModule,
    EmployerRoutingModule
  ]
})
export class EmployerModule { }
