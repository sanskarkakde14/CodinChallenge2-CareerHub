using System;
namespace CareerHub.Exceptions
{
    public class FileUploadException : Exception
    {
        public FileUploadException(string message) : base(message) { }
    }
}

