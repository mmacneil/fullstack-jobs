import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AutofocusDirective } from './directives/auto-focus.directive';
import { MaterialModule } from '../material/material.module';
import { MatDialogModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';


@NgModule({
  imports: [CommonModule, MaterialModule, MatDialogModule, FlexLayoutModule],
  declarations: [AutofocusDirective],
  exports: [AutofocusDirective, MaterialModule, FlexLayoutModule],
  providers: [],
  entryComponents: [     
  ],
})
export class SharedModule { }