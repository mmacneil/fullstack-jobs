import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MaterialModule } from '../material/material.module';
import { ApplicantRoutingModule } from './applicant-routing.module';
import { ApplicationConfirmationComponent } from './application-confirmation/application-confirmation.component';

@NgModule({
  declarations: [ApplicationConfirmationComponent],
  imports: [
    CommonModule,
    FlexLayoutModule,
    MaterialModule,
    ApplicantRoutingModule
  ]
})
export class ApplicantModule { }
