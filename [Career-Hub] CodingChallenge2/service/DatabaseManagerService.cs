using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.model;
using CareerHub.Exceptions;
using CareerHub.repository;

namespace CareerHub.service
{
    public class CareerHubService
    {
        private readonly DatabaseManagerRepository _databaseManager;

        public CareerHubService(DatabaseManagerRepository databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public void InsertCompany()
        {
            try
            {
                Console.WriteLine("Enter Company Name:");
                string companyName = Console.ReadLine();
                Console.WriteLine("Enter Location:");
                string location = Console.ReadLine();
                Company company = new Company
                {
                    CompanyName = companyName,
                    Location = location
                };
                _databaseManager.InsertCompany(company);
                Console.WriteLine("Company added successfully.");
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error connecting to the database: {ex.Message}");
            }
        }

        public void InsertApplicant()
        {
            try
            {
                Console.WriteLine("Enter First Name:");
                string firstName = Console.ReadLine();
                Console.WriteLine("Enter Last Name:");
                string lastName = Console.ReadLine();
                Console.WriteLine("Enter Email:");
                string email = Console.ReadLine();

        
                if (!IsValidEmail(email))
                {
                    throw new InvalidEmailFormatException("Invalid email format. Please enter a valid email address.");
                }

                Console.WriteLine("Enter Phone:");
                string phone = Console.ReadLine();
                Applicant applicant = new Applicant
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = phone
                };
                _databaseManager.InsertApplicant(applicant);
                Console.WriteLine("Applicant added successfully.");
            }
            catch (InvalidEmailFormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error connecting to the database: {ex.Message}");
            }
        }

        public void InsertJobListing()
        {
            try
            {
                Console.WriteLine("Enter Company ID:");
                int companyId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Job Title:");
                string jobTitle = Console.ReadLine();
                Console.WriteLine("Enter Job Description:");
                string jobDescription = Console.ReadLine();
                Console.WriteLine("Enter Job Location:");
                string jobLocation = Console.ReadLine();
                Console.WriteLine("Enter Salary:");
                decimal salary = Convert.ToDecimal(Console.ReadLine());

           
                if (salary < 0)
                {
                    throw new NegativeSalaryException("Salary cannot be negative. Please enter a valid salary.");
                }

                Console.WriteLine("Enter Job Type:");
                string jobType = Console.ReadLine();

                JobListing job = new JobListing
                {
                    CompanyID = companyId,
                    JobTitle = jobTitle,
                    JobDescription = jobDescription,
                    JobLocation = jobLocation,
                    Salary = salary,
                    JobType = jobType,
                    PostedDate = DateTime.Now
                };

                _databaseManager.InsertJobListing(job);
                Console.WriteLine("Job listing added successfully.");
            }
            catch (NegativeSalaryException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error connecting to the database: {ex.Message}");
            }
        }

        public void InsertJobApplication()
        {
            try
            {
                Console.WriteLine("Enter Job ID:");
                int jobId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Applicant ID:");
                int applicantId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Cover Letter:");
                string coverLetter = Console.ReadLine();

               
                DateTime applicationDeadline = DateTime.Now.AddDays(7); 
                if (DateTime.Now > applicationDeadline)
                {
                    throw new ApplicationDeadlineException("The application deadline has passed. No applications will be accepted.");
                }

                JobApplication application = new JobApplication
                {
                    JobID = jobId,
                    ApplicantID = applicantId,
                    ApplicationDate = DateTime.Now,
                    CoverLetter = coverLetter
                };

                _databaseManager.InsertJobApplication(application);
                Console.WriteLine("Job application submitted successfully.");
            }
            catch (ApplicationDeadlineException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error connecting to the database: {ex.Message}");
            }
        }

        public void UploadResume()
        {
            try
            {
                Console.WriteLine("Enter file path to upload resume:");
                string filePath = Console.ReadLine();

        
                if (!System.IO.File.Exists(filePath))
                {
                    throw new FileUploadException("File not found. Please provide a valid file path.");
                }

                long fileSize = new System.IO.FileInfo(filePath).Length;
                if (fileSize > 5000000) 
                {
                    throw new FileUploadException("File size exceeds the limit of 5 MB.");
                }

                
                if (!filePath.EndsWith(".pdf"))
                {
                    throw new FileUploadException("File format not supported. Please upload a .pdf file.");
                }


                Console.WriteLine("Resume uploaded successfully.");
            }
            catch (FileUploadException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        public void DisplayJobListings()
        {
            try
            {
                List<JobListing> jobListings = _databaseManager.GetJobListings();
                Console.WriteLine("Current Job Listings:");
                foreach (var job in jobListings)
                {
                    Console.WriteLine($"ID: {job.JobID}, Title: {job.JobTitle}, Company ID: {job.CompanyID}, Location: {job.JobLocation}, Salary: {job.Salary}, Type: {job.JobType}");
                }
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error connecting to the database: {ex.Message}");
            }
        }

        public void DisplayCompanies()
        {
            try
            {
                List<Company> companies = _databaseManager.GetCompanies();
                Console.WriteLine("Registered Companies:");
                foreach (var company in companies)
                {
                    Console.WriteLine($"ID: {company.CompanyID}, Name: {company.CompanyName}, Location: {company.Location}");
                }
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error connecting to the database: {ex.Message}");
            }
        }

        public void DisplayApplicants()
        {
            try
            {
                List<Applicant> applicants = _databaseManager.GetApplicants();
                Console.WriteLine("Registered Applicants:");
                foreach (var applicant in applicants)
                {
                    Console.WriteLine($"ID: {applicant.ApplicantID}, Name: {applicant.FirstName} {applicant.LastName}, Email: {applicant.Email}, Phone: {applicant.Phone}");
                }
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error connecting to the database: {ex.Message}");
            }
        }

        public void DisplayApplicationsForJob()
        {
            try
            {
                Console.WriteLine("Enter Job ID to view applications:");
                int jobId = Convert.ToInt32(Console.ReadLine());
                List<JobApplication> applications = _databaseManager.GetApplicationsForJob(jobId);
                Console.WriteLine($"Applications for Job ID {jobId}:");
                foreach (var application in applications)
                {
                    Console.WriteLine($"Application ID: {application.ApplicationID}, Applicant ID: {application.ApplicantID}, Date: {application.ApplicationDate}");
                }
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error connecting to the database: {ex.Message}");
            }
        }


        private bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

    }
}