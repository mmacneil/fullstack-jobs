import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators'
import { JobSummary } from '../../core/models/job-summary';
import { DialogComponent } from '../../shared/dialog/dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { EmployerJobsGQL } from '../../core/graphql/queries/employer-jobs.gql';
import { CreateJobGQL } from '../../core/graphql/mutations/create-job.gql';
import { Router } from '@angular/router';

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.scss']
})
export class JobsComponent implements OnInit {

  constructor(private router: Router, private employerJobsGQL: EmployerJobsGQL, private createJobGQL: CreateJobGQL, private dialog: MatDialog) { }

  busy = false;
  jobs: JobSummary[];

  createJob() {
    this.createJobGQL.mutate().subscribe((result: { data: { [x: string]: any; }; }) => {
      let job = result.data['createJob'];
      this.router.navigate(['/employer', 'job', job.id, 'manage', 'basics']);
    });
  }

  ngOnInit() {

    this.busy = true;
    this.employerJobsGQL.fetch(null, { fetchPolicy: 'network-only' }).pipe(finalize(() => {
      this.busy = false;
    })).subscribe(result => {
      this.jobs = result.data['employerJobs'];
    }, (error: any) => {
      this.dialog.open(DialogComponent, {
        width: '250px',
        data: { title: 'Oops!', message: error }
      });
    });

  }
}
