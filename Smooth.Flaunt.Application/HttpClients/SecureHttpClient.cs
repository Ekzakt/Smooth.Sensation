namespace Smooth.Client.Application.HttpClients;

public class SecureHttpClient
{
    public HttpClient Client { get; }

    public SecureHttpClient(HttpClient client)
    {
        Client = client;
    }
}
