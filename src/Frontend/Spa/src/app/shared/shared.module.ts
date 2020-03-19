import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AutofocusDirective } from './directives/auto-focus.directive';
import { MaterialModule } from '../material/material.module';
import { MatDialogModule } from '@angular/material/dialog';
import { FlexLayoutModule } from '@angular/flex-layout';
import { DialogComponent } from './dialog/dialog.component';


@NgModule({
  imports: [CommonModule, MaterialModule, MatDialogModule, FlexLayoutModule],
  declarations: [AutofocusDirective, DialogComponent],
  exports: [AutofocusDirective, MaterialModule, FlexLayoutModule],
  providers: [],
  entryComponents: [ 
    DialogComponent    
  ],
})
export class SharedModule { }