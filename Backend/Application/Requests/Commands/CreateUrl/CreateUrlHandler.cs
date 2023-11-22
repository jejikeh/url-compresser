using Application.Common;
using Application.Common.Configuration;
using Application.Services;
using UrlCompressor.Domain;

namespace Application.Requests.Commands.CreateUrl;

public class CreateUrlHandler
{
    private readonly ApplicationConfiguration _applicationConfiguration;
    private readonly ICompressedUrlRepository _compressedUrlRepository;
    private readonly IUrlCompressorService _urlCompressorService;

    public CreateUrlHandler(IUrlCompressorService urlCompressorService, ICompressedUrlRepository compressedUrlRepository,
        IApplicationConfiguration applicationConfiguration)
    {
        _urlCompressorService = urlCompressorService;
        _compressedUrlRepository = compressedUrlRepository;
        _applicationConfiguration = applicationConfiguration.ApplicationConfiguration;
    }

    public async Task<CompressedUrl> HandleAsync(CreateUrlRequest request, CancellationToken cancellationToken)
    {
        if (_applicationConfiguration.RequireUniqueUrls 
            && await _compressedUrlRepository.GetByUrlAsync(request.InitialUrl, cancellationToken) is not null)
        {
            throw new HttpException(409, "Url already exists");
        }

        var smallUrl = _urlCompressorService.CompressUrl(request.InitialUrl);
        var compressedUrl = new CompressedUrl
        {
            Id = Guid.NewGuid(),
            InitialUrl = request.InitialUrl,
            SmallUrl = smallUrl,
            CreatedAt = DateTime.UtcNow
        };

        await _compressedUrlRepository.AddAsync(compressedUrl, cancellationToken);
        await _compressedUrlRepository.SaveChangesAsync(cancellationToken);
        
        return compressedUrl;
    }
}