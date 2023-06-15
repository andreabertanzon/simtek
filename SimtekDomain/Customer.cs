namespace SimtekDomain;

public record Customer(
    int Id,
    string? Name,
    string Surname,
    string Address,
    string? Vat,
    string? Email,
    string? PhoneNumber);