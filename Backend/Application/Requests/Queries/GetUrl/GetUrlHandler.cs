using Application.Common;
using Application.Common.Configuration;
using Application.Services;
using UrlCompressor.Domain;

namespace Application.Requests.Queries.GetUrl;

public class GetUrlHandler
{
    private readonly ICompressedUrlRepository _compressedUrlRepository;
    private readonly ApplicationConfiguration _applicationConfiguration;

    public GetUrlHandler(ICompressedUrlRepository compressedUrlRepository, IApplicationConfiguration applicationConfiguration)
    {
        _compressedUrlRepository = compressedUrlRepository;
        _applicationConfiguration = applicationConfiguration.ApplicationConfiguration;
    }

    public Task<CompressedUrl> HandleAsync(GetUrlRequest request, CancellationToken cancellationToken)
    {
        return _compressedUrlRepository.FindCompressedUrlByIdOrUrlAsync(request.Key, cancellationToken, _applicationConfiguration);
    }
}