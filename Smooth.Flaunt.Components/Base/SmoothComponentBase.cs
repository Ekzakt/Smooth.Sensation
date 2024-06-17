using Microsoft.AspNetCore.Components;
using Smooth.Components.Interfaces;

namespace Smooth.Flaunt.Components;

public class SmoothComponentBase : ComponentBase, IStateHasChanged
{
    /// <summary>
    /// User class names, separated by a space.
    /// </summary>
    [Parameter]
    public string? Class { get; set; }


    /// <summary>
    /// User styles, applied on top of the component's own classes and styles.
    /// </summary>
    [Parameter]
    public string? Style { get; set; }


    /// <inheritdoc/>
    void IStateHasChanged.StateHasChanged() => StateHasChanged();
}
