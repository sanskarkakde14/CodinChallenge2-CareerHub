using CareerHub.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.repository
{
    public interface ICompanyRepository
    {
        void PostJob(int companyId, string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType);
        List<JobListing> GetJobs(int companyId);
        public List<Company> GetAllCompanies();
    }
}
