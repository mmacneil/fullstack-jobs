import { Injectable } from '@angular/core';
import { Mutation } from 'apollo-angular';
import gql from 'graphql-tag';
import { jobSummaryFieldsFragment } from '../fragments/job-summary-fields.fragment.gql';

@Injectable({
    providedIn: 'root',
})
export class CreateApplicationGQL extends Mutation {
    document = gql`
    mutation ($input: CreateApplicationInput!) {
        createApplication(input: $input){
           ...jobSummaryFields
        }                    
     }
    ${jobSummaryFieldsFragment}  
  `;
}
