import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { IndexComponent } from './index/index.component';
import { SharedModule } from '../shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';
import { JobsModule } from '../jobs/jobs.module';

@NgModule({
  declarations: [IndexComponent],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    HomeRoutingModule,
    JobsModule
  ]
})
export class HomeModule { }
