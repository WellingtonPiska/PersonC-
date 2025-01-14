namespace Person.API.Exceptions;

public class DuplicatePersonNameException : Exception
{
    public DuplicatePersonNameException(string message) : base(message)
    {
            
    }
}