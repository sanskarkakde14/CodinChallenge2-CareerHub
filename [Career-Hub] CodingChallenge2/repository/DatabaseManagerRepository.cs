using CareerHub.util;
using CareerHub.model;
using Microsoft.Data.SqlClient;


namespace CareerHub.repository


{
    public class DatabaseManagerRepository:IDatabaseManagerRepository
    {
        private readonly string _connectionString;

        public DatabaseManagerRepository()
        {
            _connectionString = DbConnUtil.GetConnectionString();
        }


        public void InsertJobListing(JobListing job)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string insertQuery = @"
                    INSERT INTO Jobs (CompanyID, JobTitle, JobDescription, JobLocation, Salary, JobType, PostedDate) 
                    VALUES (@CompanyID, @JobTitle, @JobDescription, @JobLocation, @Salary, @JobType, @PostedDate)
                ";

                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@CompanyID", job.CompanyID);
                command.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                command.Parameters.AddWithValue("@JobDescription", job.JobDescription);
                command.Parameters.AddWithValue("@JobLocation", job.JobLocation);
                command.Parameters.AddWithValue("@Salary", job.Salary);
                command.Parameters.AddWithValue("@JobType", job.JobType);
                command.Parameters.AddWithValue("@PostedDate", job.PostedDate);

                command.ExecuteNonQuery();
            }
        }

        public void InsertCompany(Company company)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string insertQuery = @"
                    INSERT INTO Companies (CompanyName, Location) 
                    VALUES (@CompanyName, @Location)
                ";

                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                command.Parameters.AddWithValue("@Location", company.Location);

                command.ExecuteNonQuery();
            }
        }

        public void InsertApplicant(Applicant applicant)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string insertQuery = @"
                    INSERT INTO Applicants (FirstName, LastName, Email, Phone) 
                    VALUES (@FirstName, @LastName, @Email, @Phone)
                ";

                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@FirstName", applicant.FirstName);
                command.Parameters.AddWithValue("@LastName", applicant.LastName);
                command.Parameters.AddWithValue("@Email", applicant.Email);
                command.Parameters.AddWithValue("@Phone", applicant.Phone);

                command.ExecuteNonQuery();
            }
        }

        public void InsertJobApplication(JobApplication application)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string insertQuery = @"
                    INSERT INTO Applications (JobID, ApplicantID, ApplicationDate, CoverLetter) 
                    VALUES (@JobID, @ApplicantID, @ApplicationDate, @CoverLetter)
                ";

                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@JobID", application.JobID);
                command.Parameters.AddWithValue("@ApplicantID", application.ApplicantID);
                command.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate);
                command.Parameters.AddWithValue("@CoverLetter", application.CoverLetter);

                command.ExecuteNonQuery();
            }
        }

        public List<JobListing> GetJobListings()
        {
            List<JobListing> jobListings = new List<JobListing>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Jobs";

                SqlCommand command = new SqlCommand(selectQuery, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        JobListing job = new JobListing
                        {
                            JobID = reader.GetInt32(0),
                            CompanyID = reader.GetInt32(1),
                            JobTitle = reader.GetString(2),
                            JobDescription = reader.GetString(3),
                            JobLocation = reader.GetString(4),
                            Salary = reader.GetDecimal(5),
                            JobType = reader.GetString(6),
                            PostedDate = reader.GetDateTime(7)
                        };

                        jobListings.Add(job);
                    }
                }
            }

            return jobListings;
        }

        public List<Company> GetCompanies()
        {
            List<Company> companies = new List<Company>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Companies";

                SqlCommand command = new SqlCommand(selectQuery, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Company company = new Company
                        {
                            CompanyID = reader.GetInt32(0),
                            CompanyName = reader.GetString(1),
                            Location = reader.GetString(2)
                        };

                        companies.Add(company);
                    }
                }
            }

            return companies;
        }

        public List<Applicant> GetApplicants()
        {
            List<Applicant> applicants = new List<Applicant>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Applicants";

                SqlCommand command = new SqlCommand(selectQuery, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Applicant applicant = new Applicant
                        {
                            ApplicantID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Email = reader.GetString(3),
                            Phone = reader.GetString(4)
                        };

                        applicants.Add(applicant);
                    }
                }
            }

            return applicants;
        }

        public List<JobApplication> GetApplicationsForJob(int jobID)
        {
            List<JobApplication> applications = new List<JobApplication>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Applications WHERE JobID = @JobID";

                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@JobID", jobID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        JobApplication application = new JobApplication
                        {
                            ApplicationID = reader.GetInt32(0),
                            JobID = reader.GetInt32(1),
                            ApplicantID = reader.GetInt32(2),
                            ApplicationDate = reader.GetDateTime(3),
                            CoverLetter = reader.GetString(4)
                        };

                        applications.Add(application);
                    }
                }
            }

            return applications;
        }
       
            
        }
    }

