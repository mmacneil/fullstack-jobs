import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { JobsRoutingModule } from './jobs-routing.module';
import { MaterialModule } from '../material/material.module';
import { MatDialogModule } from '@angular/material/dialog';
import { SharedModule } from '../shared/shared.module';
import { ListComponent } from './list/list.component'; 


@NgModule({
  declarations: [ListComponent],
  exports: [ListComponent],
  imports: [
    CommonModule,
    FlexLayoutModule,
    JobsRoutingModule,
    MaterialModule,
    MatDialogModule,
    SharedModule
  ]
})
export class JobsModule { }