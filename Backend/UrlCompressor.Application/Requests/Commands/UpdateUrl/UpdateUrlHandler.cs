using System.Net;
using UrlCompressor.Application.Common;
using UrlCompressor.Application.Common.Configuration;
using UrlCompressor.Application.Services;

namespace UrlCompressor.Application.Requests.Commands.UpdateUrl;

public class UpdateUrlHandler(
    ICompressedUrlRepository compressedUrlRepository,
    IApplicationConfiguration applicationConfiguration, 
    IUrlCompressorService compressorService)
{
    private readonly ApplicationConfiguration _applicationConfiguration = applicationConfiguration.ApplicationConfiguration;

    public async Task HandleAsync(UpdateUrlRequest request, CancellationToken cancellationToken)
    {
        var url = await compressedUrlRepository.FindCompressedUrlByIdOrUrlAsync(request.Key, cancellationToken, _applicationConfiguration);
        if (string.CompareOrdinal(request.NewInitialUrl, url.InitialUrl) == 0)
        {
            throw new HttpException(409, "Url the same!");
        }
        
        if (!Uri.IsWellFormedUriString(request.NewInitialUrl, UriKind.Absolute))
        {
            throw new HttpException(400, "Invalid url");
        }
        
        url.InitialUrl = request.NewInitialUrl;
        url.SmallUrl = _applicationConfiguration.ChangeSmallUrlOnUpdate 
            ? await compressorService.CompressUrlAsync(request.NewInitialUrl, cancellationToken) 
            : url.SmallUrl;
        
        compressedUrlRepository.UpdateUrl(url);
        await compressedUrlRepository.SaveChangesAsync(cancellationToken);
    }
}