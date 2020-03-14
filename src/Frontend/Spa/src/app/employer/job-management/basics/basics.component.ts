import { Component } from '@angular/core';
import { BaseJobManagementComponent } from '../base-job-management.component';

@Component({
  selector: 'app-basics',
  templateUrl: './basics.component.html',
  styleUrls: ['./basics.component.scss']
})

export class BasicsComponent extends BaseJobManagementComponent {

  constructor() {
    super();
  }

  next() {
    this.router.navigate(['/employer', 'job', this.job.id, 'manage', 'description']);
  }
}