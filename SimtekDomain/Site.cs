namespace SimtekDomain;

public record Site(
    int Id,
    string? Name,
    string? Address,
    Customer Customer
    );