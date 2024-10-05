using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.model;
using CareerHub.repository;
namespace CareerHub.service
{
    public class CompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public void PostJob()
        {
            Console.WriteLine("Enter Company ID:");
            int companyId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Job Title:");
            string jobTitle = Console.ReadLine();
            Console.WriteLine("Enter Job Description:");
            string jobDescription = Console.ReadLine();
            Console.WriteLine("Enter Job Location:");
            string jobLocation = Console.ReadLine();
            Console.WriteLine("Enter Salary:");
            decimal salary = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter Job Type:");
            string jobType = Console.ReadLine();

            _companyRepository.PostJob(companyId, jobTitle, jobDescription, jobLocation, salary, jobType);
            Console.WriteLine("Job posted successfully.");
        }

        public void GetJobs()
        {
            List<Company> companies = _companyRepository.GetAllCompanies(); 

            Console.WriteLine("Available Companies:");
            foreach (var company in companies)
            {
                Console.WriteLine($"Company ID: {company.CompanyID}, Name: {company.CompanyName}");
            }
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("Enter Company ID:");
            int companyId = int.Parse(Console.ReadLine());

            List<JobListing> jobs = _companyRepository.GetJobs(companyId);

            Console.WriteLine($"Jobs posted by Company ID {companyId}:");
            foreach (var job in jobs)
            {
                Console.WriteLine($"Job ID: {job.JobID}, Title: {job.JobTitle}, Description: {job.JobDescription}, Location: {job.JobLocation}, Salary: {job.Salary}, Type: {job.JobType}, Posted Date: {job.PostedDate}");
            }

        }


    }
}
