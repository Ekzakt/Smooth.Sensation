using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Polly;
using Smooth.Client.Application;
using Smooth.Client.Application.HttpClients;

namespace Smooth.Flaunt.Configuration;

public static class WebAssemblyHostBuilderExtensions
{
    public static WebAssemblyHostBuilder AddHttpClients(this WebAssemblyHostBuilder builder)
    {
        var apiBaseAddress = builder.Configuration
            .GetValue<string>(Constants.API_BASE_ADDRESS_CONFIG_NAME);


        apiBaseAddress ??= builder.HostEnvironment.BaseAddress;

        var retryTimeSpans = new[]
        {
            TimeSpan.FromSeconds(1),
            TimeSpan.FromSeconds(2.5),
            TimeSpan.FromSeconds(5)
        };

        builder.Services
            .AddHttpClient<SecureHttpClient>(config =>
                {
                    config.BaseAddress = new Uri(apiBaseAddress);
                    config.DefaultRequestHeaders.Add("X-Correlation-Id", Guid.NewGuid().ToString());
                    config.Timeout = TimeSpan.FromSeconds(300);
                })
            .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(retryTimeSpans));
            //.AddHttpMessageHandler<ApiAuthorizationMessageHandler>();


        builder.Services
            .AddHttpClient<PublicHttpClient>(config =>
            {
                config.BaseAddress = new Uri(apiBaseAddress);
                config.DefaultRequestHeaders.Add("X-Correlation-Id", Guid.NewGuid().ToString());
                config.Timeout = TimeSpan.FromSeconds(300);
            })
            .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(retryTimeSpans));

        return builder;
    }


    public static WebAssemblyHostBuilder AddMsalAuthentication(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddMsalAuthentication(options =>
        {
            builder.Configuration.Bind(Constants.AZUREB2C_CONFIG_NAME, options.ProviderOptions.Authentication);

            options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
            options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");

            // TODO: Place this in some variables (appsettings, environmentvariables).
            options.ProviderOptions.DefaultAccessTokenScopes.Add("https://ekzaktb2cdev.onmicrosoft.com/5835b0d0-9e03-4b6c-97c4-4213c87f3808/api_fullaccess");

            options.ProviderOptions.LoginMode = "redirect";
        });

        return builder;
    }
}
