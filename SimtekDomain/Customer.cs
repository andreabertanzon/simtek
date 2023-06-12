namespace SimtekDomain;

public record Customer(
    int Id,
    string Name,
    string Surname,
    List<Site> Sites);