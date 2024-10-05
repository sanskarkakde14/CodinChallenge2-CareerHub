using System;
using CareerHub.repository;
using CareerHub.service;



namespace CareerHub
{
    public class MainModule
    {
        private readonly JobListingService jobListingService;
        private readonly CompanyService companyService;
        private readonly ApplicantService applicantService;
        private readonly CareerHubService careerHubService;

        public MainModule()
        {
            IJobListingRepository jobListingRepository = new JobListingRepository();
            jobListingService = new JobListingService(jobListingRepository);
            ICompanyRepository companyRepository = new CompanyRepository();
            companyService = new CompanyService(companyRepository);
            IApplicantRepository applicantRepository = new ApplicantRepository();
            applicantService = new ApplicantService(applicantRepository);
            var databaseManagerRepository = new DatabaseManagerRepository();
            careerHubService = new CareerHubService(databaseManagerRepository);
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Enter your role (1: Admin, 2: User, 0: Exit):");
                if (!int.TryParse(Console.ReadLine(), out int role))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (role)
                {
                    case 1:
                        Console.WriteLine("Welcome Admin!");
                        AdminMenu();
                        break;
                    case 2:
                        Console.WriteLine("Welcome User!");
                        UserMenu();
                        break;
                    case 0:
                        Console.WriteLine("Thank you for using the Job Listing Management System. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid role!");
                        break;
                }
            }
        }

        private void AdminMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nAdmin Menu:");
                Console.WriteLine("1. Insert Job Listing");
                Console.WriteLine("2. Insert Company");
                Console.WriteLine("3. Get Jobs");
                Console.WriteLine("4. Search jobs by salary range");
                Console.WriteLine("5. Exit to Main Menu");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        careerHubService.InsertJobListing();
                        break;
                    case 2:
                        careerHubService.InsertCompany();
                        break;
                    case 3:
                        companyService.GetJobs();
                        break;
                    case 4:
                        jobListingService.GetJobsBySalaryRange();
                        break;
                    case 5:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }

        private void UserMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nUser Menu:");
                Console.WriteLine("1. Get Applicants");
                Console.WriteLine("2. Apply");
                Console.WriteLine("3. Create Profile");
                Console.WriteLine("4. Apply for Job");
                Console.WriteLine("5. Get Jobs");
                Console.WriteLine("6. Search jobs by salary range");
                Console.WriteLine("7. Exit to Main Menu");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        jobListingService.GetApplicants();
                        break;
                    case 2:
                        jobListingService.Apply();
                        break;
                    case 3:
                        applicantService.CreateProfile();
                        break;
                    case 4:
                        applicantService.ApplyForJob();
                        break;
                    case 5:
                        companyService.GetJobs();
                        break;
                    case 6:
                        jobListingService.GetJobsBySalaryRange();
                        break;
                    case 7:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }
    }
}
