namespace SimtekDomain;

public record Material(
    int Id,
    double Price,
    string Unit,
    double Quantity)
{
    public double TotalPrice => Price * Quantity;
};