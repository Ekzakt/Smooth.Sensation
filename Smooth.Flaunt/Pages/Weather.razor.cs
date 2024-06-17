using Microsoft.AspNetCore.Components;
using Smooth.Client.Application.HttpClients;
using Smooth.Client.Application.Managers;
using Smooth.Shared.Endpoints;
using Smooth.Shared.Models;

namespace Smooth.Flaunt.Pages;

public partial class Weather : IDisposable
{

    [Inject]
    public SecureHttpClient _httpClient { get; set; }

    [Inject]
    public IHttpDataManager _httpDataManager { get; set; }

    [Inject]
    public NavigationManager _navigationManager { get; set; }

    [Inject]
    public IConfiguration _configuration { get; set; }

    [Parameter]
    public int? R { get; set; } = 10;


    private CancellationTokenSource cancellationToken = new();
    private List<WeatherForecastDto>? weatherForecastsList = new();


    protected override async Task OnInitializedAsync()
    {
        await GetWeartherForecasts();
    }




    #region Helpers

    private async Task GetWeartherForecasts()
    {
        var endpoint = EndPoints.GET_WEATHERFORECASTS(R);

        var result = await _httpDataManager.GetDataAsync<List<WeatherForecastDto>>(
            endpoint: endpoint,
            cancellationToken: cancellationToken.Token,
            usePublicHttpClient: true);

        if (result is not null)
        {
            weatherForecastsList = result ?? new List<WeatherForecastDto>();
            R = weatherForecastsList.Count;
        }

    }


    void IDisposable.Dispose()
    {
        cancellationToken.Cancel();
        cancellationToken.Dispose();
    }


    #endregion Helpers
}
