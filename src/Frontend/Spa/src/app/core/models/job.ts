import { Tag } from './tag';

export interface Job {
  id: number;
  company: string;
  position: string;
  location: string;
  annualBaseSalary: number;
  description: string;
  responsibilities: string;
  requirements: string;
  applicationInstructions: string;
  tags: Tag[];
  modified: string;
  status: string;
  icon: string;
  applicantCount: number;
}