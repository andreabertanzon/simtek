namespace SimtekDomain;

public record Site(
    Guid Id,
    string Name,
    Address? Address,
    Customer Customer
    );