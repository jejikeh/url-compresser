using UrlCompressor.Application.Common;
using UrlCompressor.Application.Common.Configuration;
using UrlCompressor.Application.Services;
using UrlCompressor.Domain;

namespace UrlCompressor.Application.Requests.Commands.DeleteUrl;

public class DeleteUrlHandler(ICompressedUrlRepository compressedUrlRepository, IApplicationConfiguration applicationConfiguration)
{
    private readonly ApplicationConfiguration _applicationConfiguration = applicationConfiguration.ApplicationConfiguration;

    public async Task HandleAsync(DeleteUrlRequest request, CancellationToken cancellationToken)
    {
        var url = await compressedUrlRepository.FindCompressedUrlByIdOrUrlAsync(request.Key, cancellationToken, _applicationConfiguration);
        compressedUrlRepository.Delete(url);
        await compressedUrlRepository.SaveChangesAsync(cancellationToken);
    }
}