import { Component } from '@angular/core';
import { BaseJobManagementComponent } from '../base-job-management.component';
import { Status } from '../../../core/models/status';
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
    this.updateJobGQL.mutate({
      input: {
        id: this.job.id,
        company: this.job.company,
        position: this.job.position,
        location: this.job.location,
        annualBaseSalary: this.job.annualBaseSalary,
        description: this.job.description,
        responsibilities: this.job.responsibilities,
        requirements: this.job.requirements,
        applicationInstructions: this.job.applicationInstructions,
        status: this.job.status,
        tags: this.job.tags
      }
    }).subscribe(
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
    { value: 'DELETED', name: 'Deleted' }
  ];
}
