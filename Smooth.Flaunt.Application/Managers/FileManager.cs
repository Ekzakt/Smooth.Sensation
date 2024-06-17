using Microsoft.AspNetCore.Components.Forms;
using Smooth.Shared.Endpoints;
using Smooth.Shared.Models.Requests;
using Smooth.Shared.Models.Responses;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Smooth.Client.Application.Managers;

public class FileManager : IFileManager
{
    private readonly IHttpDataManager _httpDataManager;


    public FileManager(IHttpDataManager dataMananger)
    {
        _httpDataManager = dataMananger;
    }


    public async Task<Guid> SaveFileAsync(IBrowserFile file, Guid id, CancellationToken cancellationToken = default)
    {
        using var content = new MultipartFormDataContent();

        var fileContent = new StreamContent(file.OpenReadStream(file.Size));

        fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

        content.Add(
            content: fileContent,
            name: "\"file\"",
            fileName: file.Name);

        var endpoint = EndPoints.POST_FILE(Guid.NewGuid().ToString());

        var result = await _httpDataManager.PostHttpContent<PostFileResponse>(endpoint, content, true, cancellationToken);

        return result?.FileId ?? Guid.Empty;
    }


    public async Task<Guid> SaveFileStreamAsync(IBrowserFile file, Guid id, CancellationToken cancellationToken = default)
    {
        using var content = new MultipartFormDataContent();

        using var fileContent = new StreamContent(file.OpenReadStream(file.Size));

        fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

        var jsonContent = JsonContent.Create(new SaveFileFormContentRequest
        {
            Id = id,
            FileContentType = file.ContentType,
            InitialFileSize = file.Size
        });

        content.Add(
            content: jsonContent,
            name: "\"jsonContant\"");

        content.Add(
            content: fileContent,
            name: "\"fileContent\"",
            fileName: file.Name);

        var endpoint = EndPoints.POST_FILE_STREAM();

        var result = await _httpDataManager.PostHttpContent<PostFileResponse>(endpoint, content, true, cancellationToken);

        return result?.FileId ?? Guid.Empty;
    }
}