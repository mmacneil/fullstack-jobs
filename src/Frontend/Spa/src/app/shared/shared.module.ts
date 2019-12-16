import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { MatDialogModule } from '@angular/material';


@NgModule({
  imports: [CommonModule, MaterialModule, MatDialogModule],
  declarations: [],
  exports: [],
  providers: [],
  entryComponents: [
     
  ],
})
export class SharedModule { }