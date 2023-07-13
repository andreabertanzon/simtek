namespace SimtekDomain.Models;

public record Site(
    int Id,
    string? Name,
    string? Address,
    Customer Customer
    );