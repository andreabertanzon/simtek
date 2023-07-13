using SimtekMaui.Models;

namespace SimtekMaui.Utils;

public record NewInterventionState(
    Customer? Customer = null,
    Site? Site = null
);