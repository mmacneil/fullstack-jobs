import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ShellComponent } from './shell.component';
import { MaterialModule} from '../material/material.module';
import { HeaderComponent } from './header/header.component';


@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    MaterialModule
  ],  
  declarations: [ShellComponent, HeaderComponent] 
})
export class ShellModule { }