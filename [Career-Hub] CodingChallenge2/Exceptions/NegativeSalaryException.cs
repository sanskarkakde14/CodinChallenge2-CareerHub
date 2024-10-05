using System;
namespace CareerHub.Exceptions
{
    public class NegativeSalaryException : Exception
    {
        public NegativeSalaryException(string message) : base(message) { }
    }
}

