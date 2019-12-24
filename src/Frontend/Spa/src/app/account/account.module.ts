import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule} from '../material/material.module';
import { SignupComponent } from './signup/signup.component';
import { SharedModule }   from '../shared/shared.module';
import { AccountRoutingModule } from './account-routing.module';


@NgModule({
  declarations: [SignupComponent],
  providers: [],
  imports: [
    CommonModule,   
    ReactiveFormsModule,
    MaterialModule,    
    AccountRoutingModule,
    SharedModule
  ]
})
export class AccountModule { }