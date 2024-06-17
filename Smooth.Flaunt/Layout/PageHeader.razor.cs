using Microsoft.AspNetCore.Components;

namespace Smooth.Flaunt.Layout;

public partial class PageHeader
{
    [Parameter]
    public string Title { get; set; } = string.Empty;
}
