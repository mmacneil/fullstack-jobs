import { Component } from '@angular/core';
import { Job } from '../../core/models/job';
import { AppInjector } from '../../app-injector.service';
import { UpdateJobGQL } from '../../core/graphql/mutations/update-job.gql';
import { EmployerJobGQL } from '../../core/graphql/queries/employer-job.gql';
import { Router } from '@angular/router';

@Component({
    selector: 'app-base-job-management',
    template: ''
})
export abstract class BaseJobManagementComponent {

    protected router: Router;
    protected updateJobGQL: UpdateJobGQL;
    protected employerJobGQL: EmployerJobGQL;
    public busy: Boolean;
    public job: Job;

    constructor() {
        // https://devblogs.microsoft.com/premier-developer/angular-how-to-simplify-components-with-typescript-inheritance/
        // Manually retrieve the dependencies from the injector so that subclass ctors contain no dependencies that must be passed in from child    
        const injector = AppInjector.getInjector();
        this.router = injector.get(Router);
        this.updateJobGQL = injector.get(UpdateJobGQL);
        this.employerJobGQL = injector.get(EmployerJobGQL);
    }
}