using CareerHub.model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CareerHub.util;

namespace CareerHub.repository
{

    public class JobListingRepository : IJobListingRepository
    {
        private readonly string _connectionString;

        public JobListingRepository()
        {
            _connectionString = DbConnUtil.GetConnectionString();
        }

        public void Apply(int jobID, int applicantID, string coverLetter)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("INSERT INTO Applications (JobID, ApplicantID, CoverLetter, ApplicationDate) ");
                queryBuilder.Append("VALUES (@JobID, @ApplicantID, @CoverLetter, @ApplicationDate)");

                using (SqlCommand command = new SqlCommand(queryBuilder.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@JobID", jobID);
                    command.Parameters.AddWithValue("@ApplicantID", applicantID);
                    command.Parameters.AddWithValue("@CoverLetter", coverLetter);
                    command.Parameters.AddWithValue("@ApplicationDate", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Applicant> GetApplicants(int jobID)
        {
            List<Applicant> applicants = new List<Applicant>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Applicants WHERE ApplicantID IN (SELECT ApplicantID FROM Applications WHERE JobID = @JobID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@JobID", jobID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Applicant applicant = new Applicant
                            {
                                ApplicantID = Convert.ToInt32(reader["ApplicantID"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Phone = reader["Phone"].ToString(),
              

                            };
                            applicants.Add(applicant);
                        }
                    }
                }
            }

            return applicants;
        }
        public List<JobListing> getjobsbysalaryrange(decimal minsalary, decimal maxsalary)
        {
            List<JobListing> jobListings = new List<JobListing>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT * FROM Jobs WHERE Salary BETWEEN @MinSalary AND @MaxSalary";
                        cmd.Parameters.AddWithValue("@MinSalary", minsalary);
                        cmd.Parameters.AddWithValue("@MaxSalary", maxsalary);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                JobListing job = new JobListing
                                {
                                    JobID = (int)reader["JobID"],
                                    CompanyID = (int)reader["CompanyID"],
                                    JobTitle = (string)reader["JobTitle"],
                                    JobDescription = (string)reader["JobDescription"],
                                    JobLocation = (string)reader["JobLocation"],
                                    JobType = (string)reader["JobType"],
                                    PostedDate = (DateTime)reader["PostedDate"]
                                };
                                if (reader["Salary"] != DBNull.Value)
                                {
                                    job.Salary = (decimal)reader["Salary"];
                                }
                                else
                                {
                                    job.Salary = 0;
                                }
                                jobListings.Add(job);


                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"An error occurred while retrieving job listings: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }

            return jobListings;
        }
    }
}

