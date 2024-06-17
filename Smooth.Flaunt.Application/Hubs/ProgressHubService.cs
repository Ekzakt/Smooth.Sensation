using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Smooth.Shared.Models.HubMessages;

namespace Smooth.Client.Application.Hubs;

public class ProgressHubService : AbstractHubService
{
    protected override string HubEndpoint => Shared.Endpoints.Hubs.PROGRESS_HUB;

    public event Action<ProgressHubMessage>? ProgressChanged;

    public ProgressHubService(
        IConfiguration configuration,
        NavigationManager navigationManager) : base(configuration, navigationManager)
    {
        HubConnection.On<ProgressHubMessage>("ProgressUpdated", messsage =>
        {
            ProgressChanged?.Invoke(messsage);
        });
    }
}
