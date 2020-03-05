import { Component, OnInit } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { finalize } from 'rxjs/operators'
import { JobSummary } from '../../core/models/job-summary';
import { DialogComponent } from '../../shared/dialog/dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.scss']
})
export class JobsComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
