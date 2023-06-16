namespace SimtekDomain.Errors;

public class SimtekError
{
    public IDomainError Error { get; }

    public SimtekError(IDomainError error)
    {
        Error = error;
    }
}