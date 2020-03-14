import { Component } from '@angular/core';
import { Job } from '../../core/models/job';
import { AppInjector } from '../../app-injector.service';
import { JobServiceGQL } from '../../shared/graphql/services/job.service.gql';
import { Router } from '@angular/router';

@Component({
    selector: 'app-base-job-management',
    template: ''
})
export abstract class BaseJobManagementComponent {

    protected busy: Boolean;
    protected router: Router;
    protected jobServiceGQL: JobServiceGQL;
    protected job: Job;

    constructor() {
        // https://devblogs.microsoft.com/premier-developer/angular-how-to-simplify-components-with-typescript-inheritance/
        // Manually retrieve the dependencies from the injector so that subclass ctors contain no dependencies that must be passed in from child    
        const injector = AppInjector.getInjector();
        this.router = injector.get(Router);
        this.jobServiceGQL = injector.get(JobServiceGQL);
    }
}