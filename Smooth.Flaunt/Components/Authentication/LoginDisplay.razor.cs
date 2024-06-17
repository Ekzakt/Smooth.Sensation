using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Smooth.Flaunt.Components.Authentication;

public partial class LoginDisplay
{
    public void BeginLogOut()
    {
        Navigation.NavigateToLogout("authentication/logout");
    }
}
