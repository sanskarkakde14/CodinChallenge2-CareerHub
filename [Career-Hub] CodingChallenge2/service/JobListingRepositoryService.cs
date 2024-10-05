using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Exceptions;
using CareerHub.model;
using CareerHub.repository;

namespace CareerHub.service
{
    public class JobListingService
    {
        private readonly IJobListingRepository _jobListingRepository;

        public JobListingService(IJobListingRepository jobListingRepository)
        {
            _jobListingRepository = jobListingRepository;
        }

        public void Apply()
        {
            Console.WriteLine("Enter Applicant ID:");
            int applicantID = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Job ID:");
            int jobID = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Cover Letter:");
            string coverLetter = Console.ReadLine();

            _jobListingRepository.Apply(applicantID, jobID, coverLetter);
            Console.WriteLine("Application submitted successfully.");
        }

        public void GetApplicants()
        {
            
            Console.WriteLine("Enter Job ID:");
            int jobID = int.Parse(Console.ReadLine());

            List<Applicant> applicants = _jobListingRepository.GetApplicants(jobID);

            Console.WriteLine("Applicants for Job:");
            foreach (var applicant in applicants)
            {
                Console.WriteLine($"ApplicantID: {applicant.ApplicantID}, Name: {applicant.FirstName} {applicant.LastName}, Email: {applicant.Email}, Phone: {applicant.Phone}");
            }
        }
        public void GetJobsBySalaryRange()
        {
            try
            {
                Console.WriteLine("Enter the minimum salary:");
                decimal minSalary = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Enter the maximum salary:");
                decimal maxSalary = decimal.Parse(Console.ReadLine());

                var jobs = _jobListingRepository.getjobsbysalaryrange(minSalary,maxSalary);

                if (jobs.Count == 0)
                {
                    Console.WriteLine("No jobs found in this salary range.");
                }
                else
                {
                    Console.WriteLine("\nJobs in the salary range:");
                    foreach (var job in jobs)
                    {
                        Console.WriteLine($"Job Title: {job.JobTitle} || Salary: {job.Salary}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving jobs by salary range: {ex.Message}");
            }
        }
    }
}
