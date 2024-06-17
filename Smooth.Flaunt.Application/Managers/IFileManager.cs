using Microsoft.AspNetCore.Components.Forms;

namespace Smooth.Client.Application.Managers;

public interface IFileManager
{
    Task<Guid> SaveFileAsync(IBrowserFile file, Guid id, CancellationToken cancellationToken = default);

    Task<Guid> SaveFileStreamAsync(IBrowserFile file, Guid id, CancellationToken cancellationToken = default);
}