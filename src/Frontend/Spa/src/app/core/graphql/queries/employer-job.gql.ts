import { Injectable } from '@angular/core';
import { Query } from 'apollo-angular';
import gql from 'graphql-tag';
import { JobResponse } from '../models/job-response';
import { jobFieldsFragment } from '../fragments/job-fields.fragment.gql';

@Injectable({
    providedIn: 'root',
})
export class EmployerJobGQL extends Query<JobResponse> {
    document = gql`
  query FullStackJobsQuery($id: Int!)
  {           
      job(id: $id) {
        ...jobFields
        annualBaseSalary                        
      }            
  }, 
  ${jobFieldsFragment} 
  `;
}