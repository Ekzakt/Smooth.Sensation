using Microsoft.AspNetCore.Components;
using Smooth.Client.Application.Hubs;
using Smooth.Shared.Configurations;

namespace Smooth.Flaunt.Layout;

public partial class MainLayout : LayoutComponentBase, IAsyncDisposable
{
    [Inject]
    public NotificationsHubService? _notificationsHubService { get; set; }

    private AppVersions? _appVersions;
    private int _id = default;


    protected override async Task OnInitializedAsync()
    {
        var assemblyVersion = typeof(Program).Assembly?.GetName()?.Version;
        _appVersions = new AppVersions(assemblyVersion!, Environment.Version);

        await _notificationsHubService!.StartAsync();

        _notificationsHubService.MessageReceived += HandleMessageReceived;
    }


    private void HandleMessageReceived(int id)
    {
        _id = id;

        StateHasChanged();
    }


    public async ValueTask DisposeAsync()
    {
        if (_notificationsHubService is not null)
        {
            _notificationsHubService.MessageReceived -= HandleMessageReceived;
            await _notificationsHubService!.StopAsync(true);
        }
    }

}
