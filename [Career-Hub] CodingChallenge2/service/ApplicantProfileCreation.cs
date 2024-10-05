using CareerHub.Exceptions;
using CareerHub.model;
using CareerHub.util;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace CareerHub.service
{
    public class ApplicantProfileCreationService
    {
        private readonly string _connectionString;

        public ApplicantProfileCreationService()
        {
            _connectionString = DbConnUtil.GetConnectionString();
        }

        public void CreateApplicantProfile(string firstName, string lastName, string email, string phone,string resume)
        {
            try
            {
                if (!IsValidEmail(email))
                {
                    throw new InvalidEmailFormatException("Invalid email format. Please provide a valid email address.");
                }

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO Applicants (FirstName, LastName, Email, Phone,Resume) VALUES (@FirstName, @LastName, @Email, @Phone,@Resume)";
                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Resume",resume );

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Applicant profile created successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to create applicant profile.");
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Database error: {sqlEx.Message}");
            }
            catch (InvalidEmailFormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
