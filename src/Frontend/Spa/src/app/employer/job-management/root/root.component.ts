import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs/operators'
import { BaseJobManagementComponent } from '../base-job-management.component';

@Component({
  selector: 'app-root',
  templateUrl: './root.component.html',
  styleUrls: ['./root.component.scss']
})
export class RootComponent extends BaseJobManagementComponent implements OnInit {

  private activatedChild: any;

  constructor(private activatedRoute: ActivatedRoute) {
    super();
  }

  onActivate(componentRef: any) {
    this.activatedChild = componentRef;
    componentRef.job = this.job;
  }

  ngOnInit() {
    if (!this.job) {
      this.busy = true;
      this.employerJobGQL.fetch({ id: +this.activatedRoute.snapshot.paramMap.get("id") }, { fetchPolicy: 'network-only' }).pipe(finalize(() => {
        this.busy = false;
      })).subscribe(result => {
        this.job = result.data.job;
      });
    }
  }
}


