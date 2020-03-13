import { Injectable } from '@angular/core';
import { Mutation } from 'apollo-angular';
import gql from 'graphql-tag';

@Injectable({
    providedIn: 'root',
})
export class CreateJobGQL extends Mutation {
    document = gql`
    mutation ($input:  CreateJobInput) {
        createJob(input: {}){
            id
        }                    
     }      
  `;
}