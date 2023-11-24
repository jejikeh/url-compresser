namespace UrlCompressor.Application.Services;

public interface IUrlCompressorService
{
    public Task<string> CompressUrlAsync(string url, CancellationToken cancellationToken);
}