using UrlCompressor.Application.Common;
using UrlCompressor.Application.Common.Configuration;
using UrlCompressor.Application.Services;
using UrlCompressor.Domain;

namespace UrlCompressor.Application.Requests.Queries.GetUrl;

public class GetUrlHandler(ICompressedUrlRepository compressedUrlRepository, IApplicationConfiguration applicationConfiguration)
{
    private readonly ApplicationConfiguration _applicationConfiguration = applicationConfiguration.ApplicationConfiguration;

    public Task<CompressedUrl> HandleAsync(GetUrlRequest request, CancellationToken cancellationToken)
    {
        return compressedUrlRepository.FindCompressedUrlByIdOrUrlAsync(request.Key, cancellationToken, _applicationConfiguration);
    }
}