namespace SimtekMaui.Models.Exceptions;

public class NoDataFoundException:Exception
{
    public NoDataFoundException(string property) : base($"No data found for:{property}")
    {
        
    }
    
}