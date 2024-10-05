using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.model;
using CareerHub.repository;

namespace CareerHub.service
{
    public class ApplicantService
    {
        private readonly IApplicantRepository _applicantRepository;

        public ApplicantService(IApplicantRepository applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }

        public void CreateProfile()
        {
            string firstName = PromptUser("Enter First Name:");
            string lastName = PromptUser("Enter Last Name:");
            string email = PromptUser("Enter Email:");
            string phone = PromptUser("Enter Phone:");

            Console.WriteLine("\nPlease confirm your details:");
            Console.WriteLine($"First Name: {firstName}");
            Console.WriteLine($"Last Name: {lastName}");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"Phone: {phone}");
            Console.Write("Is this information correct? (y/n): ");

            string confirmation = Console.ReadLine();
            if (confirmation?.ToLower() == "y")
            {
                _applicantRepository.CreateProfile(email, firstName, lastName, phone);
                Console.WriteLine("Profile created successfully.");
            }
            else
            {
                Console.WriteLine("Profile creation canceled.");
            }
        }

        public void ApplyForJob()
        {
            int applicantID;
            int jobID;

            while (true)
            {
                if (int.TryParse(PromptUser("Enter Applicant ID:"), out applicantID) && applicantID > 0)
                    break;
                Console.WriteLine("Invalid Applicant ID. Please enter a valid positive number.");
            }

            while (true)
            {
                if (int.TryParse(PromptUser("Enter Job ID:"), out jobID) && jobID > 0)
                    break;
                Console.WriteLine("Invalid Job ID. Please enter a valid positive number.");
            }

            string coverLetter = PromptUser("Enter Cover Letter:");

            Console.WriteLine("\nPlease confirm your application details:");
            Console.WriteLine($"Applicant ID: {applicantID}");
            Console.WriteLine($"Job ID: {jobID}");
            Console.WriteLine($"Cover Letter: {coverLetter}");
            Console.Write("Is this information correct? (y/n): ");

            string confirmation = Console.ReadLine();
            if (confirmation?.ToLower() == "y")
            {
                _applicantRepository.ApplyForJob(applicantID, jobID, coverLetter);
                Console.WriteLine("Job application submitted successfully.");
            }
            else
            {
                Console.WriteLine("Job application canceled.");
            }
        }

        private string PromptUser(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
    }
}
