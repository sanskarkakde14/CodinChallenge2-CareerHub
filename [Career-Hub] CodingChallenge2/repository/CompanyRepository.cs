using CareerHub.model;
using CareerHub.util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly string _connectionString;

        public CompanyRepository()
        {
            _connectionString = DbConnUtil.GetConnectionString();
        }

        public void PostJob(int companyId, string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("INSERT INTO Jobs (CompanyId, JobTitle, JobDescription, JobLocation, Salary, JobType, PostedDate) ");
                queryBuilder.Append("VALUES (@CompanyId, @JobTitle, @JobDescription, @JobLocation, @Salary, @JobType, @PostedDate)");

                string query = queryBuilder.ToString(); // Convert StringBuilder to string

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CompanyId", companyId);
                    command.Parameters.AddWithValue("@JobTitle", jobTitle);
                    command.Parameters.AddWithValue("@JobDescription", jobDescription);
                    command.Parameters.AddWithValue("@JobLocation", jobLocation);
                    command.Parameters.AddWithValue("@Salary", salary);
                    command.Parameters.AddWithValue("@JobType", jobType);
                    command.Parameters.AddWithValue("@PostedDate", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<JobListing> GetJobs(int companyId)
        {
            List<JobListing> jobs = new List<JobListing>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Jobs WHERE CompanyId = @CompanyId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CompanyId", companyId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            JobListing job = new JobListing
                            {
                                JobID = reader.GetInt32(reader.GetOrdinal("JobID")),
                                CompanyID = reader.GetInt32(reader.GetOrdinal("CompanyId")),
                                JobTitle = reader.GetString(reader.GetOrdinal("JobTitle")),
                                JobDescription = reader.GetString(reader.GetOrdinal("JobDescription")),
                                JobLocation = reader.GetString(reader.GetOrdinal("JobLocation")),
                                Salary = reader.GetDecimal(reader.GetOrdinal("Salary")),
                                JobType = reader.GetString(reader.GetOrdinal("JobType")),
                                PostedDate = reader.GetDateTime(reader.GetOrdinal("PostedDate"))
                            };
                            jobs.Add(job);
                        }
                    }
                }
            }

            return jobs;
        }
        public List<Company> GetAllCompanies()
        {
            List<Company> companies = new List<Company>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT CompanyID, CompanyName FROM Companies"; 

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Company company = new Company
                            {
                                CompanyID = reader.GetInt32(0), 
                                CompanyName = reader.GetString(1) 
                            };
                            companies.Add(company);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving companies: {ex.Message}");
            }

            return companies;
        }

    }
}

