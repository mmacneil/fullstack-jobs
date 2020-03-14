import { Component } from '@angular/core';
import { BaseJobManagementComponent } from '../base-job-management.component';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';

@Component({
  selector: 'app-application',
  templateUrl: './application.component.html',
  styleUrls: ['./application.component.scss']
})
export class ApplicationComponent extends BaseJobManagementComponent {

  requirementsEditor = ClassicEditor;
  instructionsEditor = ClassicEditor;

  constructor() {
    super();
  }

  next() {
    this.router.navigate(['/employer', 'job', this.job.id, 'manage', 'tags']);
  }
}
