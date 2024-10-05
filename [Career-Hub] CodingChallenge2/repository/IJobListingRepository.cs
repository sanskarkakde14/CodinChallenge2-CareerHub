using CareerHub.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.repository
{
    public interface IJobListingRepository
    {
        void Apply(int applicantID, int jobID, string coverLetter);
        List<Applicant> GetApplicants(int jobID);
        List<JobListing> getjobsbysalaryrange(decimal minsalary, decimal maxsalary);
    }
}

