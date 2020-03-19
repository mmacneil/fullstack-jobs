import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { JobsRoutingModule } from './jobs-routing.module';
import { MaterialModule } from '../material/material.module';
import { MatDialogModule } from '@angular/material/dialog';
import { SharedModule } from '../shared/shared.module';
import { ApplicantModule } from '../applicant/applicant.module';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component'; 

@NgModule({
  declarations: [ListComponent, DetailsComponent],
  exports: [ListComponent],
  imports: [
    CommonModule,
    FlexLayoutModule,
    JobsRoutingModule,
    MaterialModule,
    MatDialogModule,
    SharedModule,
    ApplicantModule
  ]
})
export class JobsModule { }