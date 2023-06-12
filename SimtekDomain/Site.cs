namespace SimtekDomain;

public record Site(int Id,
    string Name,
    Address? Address,
    Customer Customer
    );
public record Address(
    string City,
    string CivicNumber,
    string Cap,
    string Street,
    string Province
);