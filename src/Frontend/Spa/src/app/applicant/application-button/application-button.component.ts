import { Component, Input } from '@angular/core';
import { CreateApplicationGQL } from '../../core/graphql/mutations/create-application.gql';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../../shared/dialog/dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'application-button',
  templateUrl: './application-button.component.html',
  styleUrls: ['./application-button.component.scss']
})
export class ApplicationButtonComponent {

  @Input() jobId: number;
  @Input() applicantCount: number;

  label: string;

  constructor(private router: Router, private dialog: MatDialog, private createApplicationGQL: CreateApplicationGQL) { }

  apply() {
    this.createApplicationGQL.mutate({
      input: {
        jobId: this.jobId
      }
    }).subscribe(
      result => {
        this.router.navigateByUrl('applicant/confirmation', { state: { data: result.data['createApplication'] } });
      }, (error: any) => {
        this.dialog.open(DialogComponent, {
          width: '250px',
          data: { title: 'Oops!', message: error }
        });
      }
    );
  }
}
