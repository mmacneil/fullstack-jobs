import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';
import { JobSummary } from '../../core/models/job-summary';

@Component({
  selector: 'app-application-confirmation',
  templateUrl: './application-confirmation.component.html',
  styleUrls: ['./application-confirmation.component.scss']
})
export class ApplicationConfirmationComponent implements OnInit {

  jobSummary: JobSummary;

  constructor(private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.paramMap.pipe(map(() => window.history.state)).subscribe(result => this.jobSummary = result.data);
  }
}
