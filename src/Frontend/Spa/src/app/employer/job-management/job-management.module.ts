import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BasicsComponent } from './basics/basics.component';
import { JobManagementRoutingModule } from './job-management-routing.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MaterialModule } from '../../material/material.module';
import { RootComponent } from './root/root.component';
import { DescriptionComponent } from './description/description.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { SharedModule }   from '../../shared/shared.module';
import { ApplicationComponent } from './application/application.component';
import { TagsComponent } from './tags/tags.component';
import { PublishComponent } from './publish/publish.component';

@NgModule({
  declarations: [BasicsComponent, RootComponent, DescriptionComponent, ApplicationComponent, TagsComponent, PublishComponent],
  imports: [
    CKEditorModule,
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule,
    JobManagementRoutingModule,
    FlexLayoutModule,
    MaterialModule
  ]
})
export class JobManagementModule { }
