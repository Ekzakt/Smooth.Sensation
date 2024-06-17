using Microsoft.AspNetCore.Components;

namespace Smooth.Flaunt.Components;

public partial class SmoothProgressLinear : SmoothComponentBase
{
    [Parameter]
    public double PercentageDone { get; set; } = 0;

    private string GetWidthStyle => $"width: {PercentageDone}%;";

}