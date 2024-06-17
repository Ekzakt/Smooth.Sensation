using Microsoft.AspNetCore.Components;

namespace Smooth.Flaunt.Components.Article;

public partial class Article
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }


    [Parameter]
    public string? Title { get; set; } = string.Empty;


    [Parameter]
    public string? Value { get; set; } = string.Empty;
}
