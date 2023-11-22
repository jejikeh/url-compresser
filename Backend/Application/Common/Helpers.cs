using Application.Common.Configuration;
using Application.Services;
using UrlCompressor.Domain;

namespace Application.Common;

public static class Helpers
{
    public static async Task<CompressedUrl> FindCompressedUrlByIdOrUrlAsync(
        this ICompressedUrlRepository compressedUrlRepository,
        string request, 
        CancellationToken cancellationToken, 
        ApplicationConfiguration applicationConfiguration)
    {
        CompressedUrl? url = null;
        if (applicationConfiguration.AllowSearchByInitialUrl)
        {
            url = await compressedUrlRepository.GetByUrlAsync(request, cancellationToken);
        }

        if (url is null)
        {
            var guidKey = Guid.TryParse(request, out var guid);
            if (!guidKey)
            {
                throw new HttpException(404, "Key is not a guid");
            }

            url = await compressedUrlRepository.GetByIdAsync(guid, cancellationToken);
        }

        if (url is null)
        {
            throw new HttpException(404, "Url not found");
        }
        
        return url;
    }
}