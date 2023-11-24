using System.Net;
using UrlCompressor.Application.Common;
using UrlCompressor.Application.Common.Configuration;
using UrlCompressor.Application.Services;
using UrlCompressor.Domain;

namespace UrlCompressor.Application.Requests.Commands.CreateUrl;

public class CreateUrlHandler(
    IUrlCompressorService urlCompressorService, 
    ICompressedUrlRepository compressedUrlRepository,
    IApplicationConfiguration applicationConfiguration)
{
    private readonly ApplicationConfiguration _applicationConfiguration = applicationConfiguration.ApplicationConfiguration;

    public async Task<CompressedUrl> HandleAsync(CreateUrlRequest request, CancellationToken cancellationToken)
    {
        if (_applicationConfiguration.RequireUniqueUrls 
            && await compressedUrlRepository.GetByUrlAsync(request.InitialUrl, cancellationToken) is not null)
        {
            throw new HttpException(409, "Url already exists");
        }
        
        if (!Uri.IsWellFormedUriString(request.InitialUrl, UriKind.Absolute))
        {
            throw new HttpException(400, "Invalid url");
        }

        var smallUrl = await urlCompressorService.CompressUrlAsync(request.InitialUrl, cancellationToken);
        var compressedUrl = new CompressedUrl
        {
            Id = Guid.NewGuid(),
            InitialUrl = request.InitialUrl,
            SmallUrl = smallUrl,
            CreatedAt = DateTime.UtcNow
        };

        compressedUrlRepository.Add(compressedUrl);
        await compressedUrlRepository.SaveChangesAsync(cancellationToken);
        
        return compressedUrl;
    }
}