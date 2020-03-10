import gql from 'graphql-tag';

export const jobFieldsFragment = gql`
  fragment jobFields on JobType {
    id
    position
    company
    icon
    location
    description
    responsibilities
    requirements
    applicationInstructions
    tags
    {
      id
      name
    }
    status
    modified
    applicantCount
  }
`;