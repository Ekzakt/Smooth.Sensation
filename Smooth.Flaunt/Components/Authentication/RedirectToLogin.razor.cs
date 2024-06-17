using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Smooth.Flaunt.Components.Authentication;

public partial class RedirectToLogin
{
    [Inject]
    private NavigationManager? _navigationMananger { get; set; }

    protected override void OnInitialized()
    {
        _navigationMananger?.NavigateToLogin("authentication/login");
    }
}
