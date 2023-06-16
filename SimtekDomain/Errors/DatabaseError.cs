namespace SimtekDomain.Errors;

public class DatabaseError:IDomainError
{
    public DatabaseError(Exception e)
    {
        Err = e;
    }

    public Exception Err { get; }
}