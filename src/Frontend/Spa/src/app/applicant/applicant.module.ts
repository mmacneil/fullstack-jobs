import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MaterialModule } from '../material/material.module';
import { ApplicantRoutingModule } from './applicant-routing.module';
import { ApplicationButtonComponent } from './application-button/application-button.component';
import { ApplicationConfirmationComponent } from './application-confirmation/application-confirmation.component';

@NgModule({
  declarations: [ ApplicationButtonComponent , ApplicationConfirmationComponent],
  imports: [
    CommonModule,
    FlexLayoutModule,
    MaterialModule,
    ApplicantRoutingModule
  ],
  exports: [ApplicationButtonComponent]
})
export class ApplicantModule { }
