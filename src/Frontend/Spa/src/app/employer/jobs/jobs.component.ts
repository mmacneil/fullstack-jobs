import { Component, OnInit } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { finalize } from 'rxjs/operators'
import { JobSummary } from '../../core/models/job-summary';
import { DialogComponent } from '../../shared/dialog/dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { EmployerJobsGQL } from '../../core/graphql/queries/employer-jobs.gql';
import { ConfigService } from '../../core/services/config.service';

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.scss']
})
export class JobsComponent implements OnInit {

  constructor(private configService: ConfigService, private employerJobsGQL: EmployerJobsGQL, private dialog: MatDialog) { }

  busy = false;
  jobs: JobSummary[];

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
