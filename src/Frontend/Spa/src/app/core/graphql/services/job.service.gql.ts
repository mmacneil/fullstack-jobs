import { Injectable } from '@angular/core';
import { EmployerJobsGQL } from '../queries/employer-jobs.gql';

@Injectable({
    providedIn: 'root'
})
export class JobServiceGQL {

    constructor(private employerJobsGQL: EmployerJobsGQL) {
    }

    getEmployerJobs() {
        return this.employerJobsGQL.fetch(null, { fetchPolicy: 'network-only' });
    }
}

