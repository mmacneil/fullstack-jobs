import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployerRoutingModule } from './employer-routing.module';
import { JobsComponent } from './jobs/jobs.component';
import { SharedModule }   from '../shared/shared.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { JobManagementModule } from './job-management/job-management.module';
import { MaterialModule} from '../material/material.module';

@NgModule({
  declarations: [JobsComponent],
  imports: [
    CommonModule,
    EmployerRoutingModule,
    SharedModule,
    JobManagementModule,
    FlexLayoutModule,
    MaterialModule
  ]
})
export class EmployerModule { }
