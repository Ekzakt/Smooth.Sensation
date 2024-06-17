using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace Smooth.Client.Application.Hubs;

public abstract class AbstractHubService
{
    public HubConnection HubConnection { get; set; }

    private readonly NavigationManager? _navigationManager;
    private readonly IConfiguration? _configuration;

    protected abstract string HubEndpoint { get; }
    private bool _isDisposed = false;

    public AbstractHubService(
        IConfiguration configuration,
        NavigationManager navigationManager)
    {
        _configuration = configuration;
        _navigationManager = navigationManager;

        HubConnection = GetHubConnection();
    }


    public async Task StartAsync()
    {
        if (_isDisposed)
        {
            HubConnection = GetHubConnection(); 
        }

        await HubConnection.StartAsync();
    }


    public async Task StopAsync(bool dispose = false)
    {
        await HubConnection.StopAsync();

        if (dispose)
        {
            await HubConnection.DisposeAsync();
            _isDisposed = true;
        }
    }




    #region Helpers

    private HubConnection GetHubConnection()
    {
        var hubConnection = new HubConnectionBuilder()
           .WithUrl(GetHubConnectionUrl(), options =>
           {
               options.Transports = HttpTransportType.WebSockets;
           })
           .WithAutomaticReconnect()
           .Build();

        _isDisposed = false;

        return hubConnection;
    }


    private string GetHubConnectionUrl()
    {
        var output = _configuration?
           .GetValue<string>(Constants.API_BASE_ADDRESS_CONFIG_NAME);

        output ??= _navigationManager?.BaseUri.TrimEnd('/');

        output = $"{output}{HubEndpoint}";

        return output;
    }


    #endregion Helpers
}
