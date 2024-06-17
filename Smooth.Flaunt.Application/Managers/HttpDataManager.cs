using Smooth.Client.Application.HttpClients;
using Smooth.Client.Application.Managers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

public class HttpDataManager : IHttpDataManager
{
    private readonly SecureHttpClient _secureHttpClient;
    private readonly PublicHttpClient _publicHttpClient;


    public HttpDataManager(SecureHttpClient secureHttpClient, PublicHttpClient publicHttpClient)
    {
        _secureHttpClient = secureHttpClient;
        _publicHttpClient = publicHttpClient;
    }


    public async Task<T?> GetDataAsync<T>(string endpoint, bool usePublicHttpClient = false, CancellationToken cancellationToken = default)
        where T : class?
    {
        HttpClient client = GetHttpClient(usePublicHttpClient);

        var response = await client.GetAsync(endpoint, cancellationToken);

        //var response = await client.GetFromJsonAsync<T>(endpoint, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<T>();
        }

        return null;
    }


    public async Task<string?> GetSerializedDataAsync<T>(string endpoint, bool usePublicHttpClient = false, CancellationToken cancellationToken = default)
        where T : class?
    {
        var result = await GetDataAsync<T>(endpoint, usePublicHttpClient, cancellationToken);

        if (result is not null)
        {
            return JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                IgnoreReadOnlyFields = false,
                WriteIndented = true
            });
        }

        return string.Empty;
    }


    public async Task<TResponse?> PostDataAsJsonAsync<TRequest, TResponse>(string endpoint, TRequest request, bool usePublicHttpClient = false, CancellationToken cancellationToken = default)
        where TResponse : class
        where TRequest : class
    {
        HttpClient client = GetHttpClient(usePublicHttpClient);

        var response = await client.PostAsJsonAsync(endpoint, request, cancellationToken);
        return await HandleResponse<TResponse>(response);
    }


    public async Task<TResponse?> PostHttpContent<TResponse>(string endpoint, HttpContent content, bool usePublicHttpClient = false, CancellationToken cancellationToken = default)
        where TResponse : class
    {
        HttpClient client = GetHttpClient(usePublicHttpClient);

        var response = await client.PostAsync(endpoint, content, cancellationToken);
        return await HandleResponse<TResponse>(response);
    }


    public async Task<T?> DeleteDataAsync<T>(string endpoint, bool usePublicHttpClient = false, CancellationToken cancellationToken = default)
        where T : class?
    {
        HttpClient client = GetHttpClient(usePublicHttpClient);

        var response = await client.DeleteAsync(endpoint, cancellationToken);
        return await HandleResponse<T>(response);
    }


    #region Helpers

    private async Task<T?> HandleResponse<T>(HttpResponseMessage response) where T : class?
    {
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<T>();
        }

        throw new HttpRequestException(response.ReasonPhrase);
    }


    private HttpClient GetHttpClient(bool usePublicHttpClient = false)
    {
        return usePublicHttpClient ? _publicHttpClient.Client : _secureHttpClient.Client;
    }

    #endregion Helpers
}
