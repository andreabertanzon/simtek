namespace SimtekDomain.Models;

public record Material(
    int Id,
    string Name,
    double Price,
    string Unit,
    double Quantity)
{
    public double TotalPrice => Price * Quantity;
};