using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.repository
{
    public interface IApplicantRepository
    {
        void CreateProfile(string email, string firstName, string lastName, string phone);
        void ApplyForJob(int applicantID, int jobID, string coverLetter);
    }
}


