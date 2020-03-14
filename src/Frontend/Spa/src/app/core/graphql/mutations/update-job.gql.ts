import { Injectable } from '@angular/core';
import { Mutation } from 'apollo-angular';
import gql from 'graphql-tag';

@Injectable({
    providedIn: 'root',
})
export class UpdateJobGQL extends Mutation {
    document = gql`
    mutation ($input: UpdateJobInput!) {
        updateJob(input: $input)                      
     } 
  `;
}