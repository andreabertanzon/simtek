using OneOf.Types;

namespace SimtekDomain.Errors;

public class NotFoundError : IDomainError
{
    public NotFoundError(string propertyName)
    {
        PropertyName = propertyName;
    }

    public string PropertyName { get; set; }
}