using System;

namespace ConfManager
{
    public class ConfManagerException : Exception
    {
        public ConfManagerException(string message) : base(message)
        {

        }

        public ConfManagerException(Exception innerException) : base(innerException.Message, innerException)
        {

        }

        public ConfManagerException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
