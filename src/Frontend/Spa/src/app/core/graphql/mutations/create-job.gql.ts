import { Injectable } from '@angular/core';
import { Mutation } from 'apollo-angular';
import { jobFieldsFragment } from '../fragments/job-fields.fragment.gql';
import gql from 'graphql-tag';

@Injectable({
    providedIn: 'root',
})
export class CreateJobGQL extends Mutation {
    document = gql`
    mutation ($input:  CreateJobInput!) {
        createJob(input: $input){
            ...jobFields
        }                    
     } 
     ${jobFieldsFragment} 
  `;
}