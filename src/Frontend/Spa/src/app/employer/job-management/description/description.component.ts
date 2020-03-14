import { Component } from '@angular/core';
import { BaseJobManagementComponent } from '../base-job-management.component';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';

@Component({
  selector: 'app-description',
  templateUrl: './description.component.html',
  styleUrls: ['./description.component.scss']
})
export class DescriptionComponent extends BaseJobManagementComponent {

  descriptionEditor = ClassicEditor;
  responsibilitiesEditor = ClassicEditor;

  constructor() {
    super();
  }

  next() {
    this.router.navigate(['/employer', 'job', this.job.id, 'manage', 'application']);
  }
}
