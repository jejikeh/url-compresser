using UrlCompressor.Application.Common;
using UrlCompressor.Application.Common.Configuration;
using UrlCompressor.Application.Services;

namespace UrlCompressor.Infrastructure.Services;

public class RandomStringService : IUrlCompressorService
{
    public Task<string> CompressUrlAsync(string url, CancellationToken cancellationToken)
    {
        return Task.Run(() => Common.Helpers.GenerateStringByRandom(6), cancellationToken);
    }
}