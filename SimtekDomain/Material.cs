namespace SimtekDomain;

public record Material(
    int Id,
    string Name,
    double Price,
    string Unit,
    double Quantity)
{
    public double TotalPrice => Price * Quantity;
};