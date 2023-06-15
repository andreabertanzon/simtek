namespace SimtekDomain;

public record Material(
    Guid Id,
    double Price,
    string Unit,
    double Quantity)
{
    public double TotalPrice => Price * Quantity;
};