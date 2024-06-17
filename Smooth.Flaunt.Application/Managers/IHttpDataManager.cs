
namespace Smooth.Client.Application.Managers;

public interface IHttpDataManager
{
    Task<T?> GetDataAsync<T>(string endpoint, bool usePublicHttpClient = false, CancellationToken cancellationToken = default)
        where T : class?;

    Task<string?> GetSerializedDataAsync<T>(string endpoint, bool usePublicHttpClient = false, CancellationToken cancellationToken = default)
        where T : class?;

    Task<TResponse?> PostDataAsJsonAsync<TRequest, TResponse>(string endpoint, TRequest request, bool usePublicHttpClient = false, CancellationToken cancellationToken = default)
        where TResponse : class
        where TRequest : class;

    Task<T?> DeleteDataAsync<T>(string endpoint, bool usePublicHttpClient = false, CancellationToken cancellationToken = default)
        where T : class?;

    Task<TResponse?> PostHttpContent<TResponse>(string endpoint, HttpContent content, bool usePublicHttpClient = false, CancellationToken cancellationToken = default) where TResponse : class;
}
