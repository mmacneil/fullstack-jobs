import { Component, OnInit } from '@angular/core';
import { Job } from '../../core/models/job';
import { ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs/operators'
import { PublicJobGQL } from '../../core/graphql/queries/public-job.gql';
import { AuthService } from '../../core/authentication/auth.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {

  job: Job;
  busy = false;

  constructor(public authService: AuthService, private activatedRoute: ActivatedRoute, private publicJobGQL: PublicJobGQL) { }

  ngOnInit() {
    this.busy = true;
    this.publicJobGQL.fetch({ id: +this.activatedRoute.snapshot.paramMap.get("id") }, { fetchPolicy: 'network-only' })
      .pipe(finalize(() => {
        this.busy = false;
      })).subscribe(result => {
        this.job = result.data.job;
      });
  }
}
