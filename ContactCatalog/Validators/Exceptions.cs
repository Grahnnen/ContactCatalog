using System;

namespace ContactCatalog.Validators
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string email) : base($"{email}") { }
    }
    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException(string email) : base($"{email}") { }
    }
    public class InvalidNameException : Exception
    {
        public InvalidNameException(string name) : base($"{name}") { }
    }
}