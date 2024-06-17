namespace Smooth.Shared.Models.Responses;

public class GetFilesListResponse
{
    public List<FileInformationDto> Files { get; set; } = new();
}
