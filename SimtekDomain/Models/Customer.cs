namespace SimtekDomain.Models;

public record Customer(
    int Id,
    string? Name,
    string Surname,
    string Address,
    string? Vat,
    string? Email,
    string? PhoneNumber) : IRecordClass
{
    public string Identity => $"{Id}-{Surname}";
};