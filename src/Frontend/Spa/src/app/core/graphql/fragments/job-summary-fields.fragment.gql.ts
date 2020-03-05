import gql from 'graphql-tag';

export const jobSummaryFieldsFragment = gql`
  fragment jobSummaryFields on JobSummaryType {
    id,
    position,
    company,
    icon,
    location,
    status,
    modified,
    applicantCount  
  }
`;