using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Smooth.Client.Application;

namespace Smooth.Flaunt.Configuration;

public class ApiAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public ApiAuthorizationMessageHandler(
        IAccessTokenProvider provider,
        NavigationManager navigation,
        IConfiguration configuration)
        : base(provider, navigation)
    {

        // TODO: Place this in some variables (appsettings, environmentvariables).
        var scope = $"https://ekzaktb2cdev.onmicrosoft.com/5835b0d0-9e03-4b6c-97c4-4213c87f3808/api_fullaccess";

        var apiBaseAddress = configuration
            .GetValue<string>(Constants.API_BASE_ADDRESS_CONFIG_NAME);

        ConfigureHandler(
            authorizedUrls: new[] { apiBaseAddress },
            scopes: new[] { scope });
    }
}