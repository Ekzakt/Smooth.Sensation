using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace Smooth.Client.Application.Hubs;

public class NotificationsHubService : AbstractHubService
{
    protected override string HubEndpoint => Shared.Endpoints.Hubs.NOTIFICATIONS_HUB;

    public event Action<int>? MessageReceived;

    public NotificationsHubService(
        IConfiguration configuration,
        NavigationManager navigationManager) : base(configuration, navigationManager)
    {
        HubConnection.On<int>("MessageReceived", id =>
        {
            MessageReceived?.Invoke(id);
        });
    }    
}
