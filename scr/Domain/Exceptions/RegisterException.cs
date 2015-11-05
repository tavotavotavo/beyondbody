using System;

namespace Domain.Exceptions
{
    public class RegisterException : Exception
    {
        public RegisterException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}