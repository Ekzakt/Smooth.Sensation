using System.Text.Json.Serialization;

namespace Smooth.Shared.Models.Requests;

public class SaveChunkedFileRequest
{
    public int ChunkIndex { get; set; }

    public long ChunkTreshold { get; set; } = 1024 * 1024;

    public long FileSize { get; set; }

    public bool Commit => ChunkTreshold * (ChunkIndex + 1) >= FileSize;
}
