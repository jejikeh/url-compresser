using UrlCompressor.Application.Common;
using UrlCompressor.Application.Common.Configuration;
using UrlCompressor.Application.Services;

namespace UrlCompressor.Application.Requests.Commands.HitUrl;

public class HitUrlHandler(ICompressedUrlRepository compressedUrlRepository, IApplicationConfiguration applicationConfiguration)
{
    private readonly ApplicationConfiguration _applicationConfiguration = applicationConfiguration.ApplicationConfiguration;

    public async Task<string> HandleAsync(HitUrlRequest request, CancellationToken cancellationToken)
    {
        var url = await compressedUrlRepository.FindUrlByCompressedUrlAsync(request.Key, cancellationToken, _applicationConfiguration);
        
        url.Hit();
        compressedUrlRepository.UpdateUrl(url);
        await compressedUrlRepository.SaveChangesAsync(cancellationToken);
        
        return url.InitialUrl;
    }
}