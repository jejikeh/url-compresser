using UrlCompressor.Application.Common.Configuration;
using UrlCompressor.Application.Services;
using UrlCompressor.Domain;

namespace UrlCompressor.Application.Common;

public static class Helpers
{
    public static async Task<CompressedUrl> FindUrlByCompressedUrlAsync(
        this ICompressedUrlRepository compressedUrlRepository,
        string request, 
        CancellationToken cancellationToken, 
        ApplicationConfiguration applicationConfiguration)
    {
        var url = await compressedUrlRepository.GetByUrlAsync(request, cancellationToken);

        if (url is null)
        {
            throw new HttpException(404, "Url not found");
        }
        
        return url;
    }
    
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