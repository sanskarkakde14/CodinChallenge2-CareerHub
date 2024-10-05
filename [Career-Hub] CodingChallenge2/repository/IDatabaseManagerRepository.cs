﻿using CareerHub.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.repository
{
    public interface IDatabaseManagerRepository
    {
        void InsertJobListing(JobListing job);
        void InsertCompany(Company company);
        void InsertApplicant(Applicant applicant);
        void InsertJobApplication(JobApplication application);
        List<JobListing> GetJobListings();
        List<Company> GetCompanies();
        List<Applicant> GetApplicants();
        List<JobApplication> GetApplicationsForJob(int jobID);
        
    }
}
