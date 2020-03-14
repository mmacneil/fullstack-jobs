import { Component } from '@angular/core';
import { BaseJobManagementComponent } from '../base-job-management.component';
import { Status } from '../../../shared/models/status';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../../../shared/dialog/dialog.component';


@Component({
  selector: 'app-publish',
  templateUrl: './publish.component.html',
  styleUrls: ['./publish.component.scss']
})
export class PublishComponent extends BaseJobManagementComponent {

  constructor(private dialog: MatDialog) {
    super();
  }

  save() {   
    this.jobServiceGQL.updateJob(this.job).subscribe(
      result => {
        this.router.navigateByUrl('/employer/jobs');
      }, (error: any) => {
        this.dialog.open(DialogComponent, {
          width: '250px',
          data: { title: 'Oops!', message: error }
        });
      }
    );
  }

  statuses: Status[] = [
    { value: 'DRAFT', name: 'Draft' },
    { value: 'PUBLISHED', name: 'Published' },
    { value: 'DELETED', name: 'Deleted'}
  ];
}
