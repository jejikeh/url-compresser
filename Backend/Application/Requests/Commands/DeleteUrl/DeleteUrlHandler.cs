using Application.Common;
using Application.Common.Configuration;
using Application.Services;
using UrlCompressor.Domain;

namespace Application.Requests.Commands.DeleteUrl;

public class DeleteUrlHandler
{
    private readonly ICompressedUrlRepository _compressedUrlRepository;
    private readonly ApplicationConfiguration _applicationConfiguration;

    public DeleteUrlHandler(ICompressedUrlRepository compressedUrlRepository, ApplicationConfiguration applicationConfiguration)
    {
        _compressedUrlRepository = compressedUrlRepository;
        _applicationConfiguration = applicationConfiguration;
    }

    public async Task HandleAsync(DeleteUrlRequest request, CancellationToken cancellationToken)
    {
        CompressedUrl? url = null;
        if (_applicationConfiguration.AllowSearchByInitialUrl)
        {
            url = await _compressedUrlRepository.GetByUrlAsync(request.Key, cancellationToken);
        } 
        
        if (url is null)
        {
            var guidKey = Guid.TryParse(request.Key, out var guid);
            if (!guidKey)
            {
                throw new HttpException(404, "Key is not a guid");
            }
            
            url = await _compressedUrlRepository.GetByIdAsync(guid, cancellationToken);
        }
        
        if (url is null)
        {
            throw new HttpException(404, "Url not found");
        }
        
        await _compressedUrlRepository.DeleteAsync(url, cancellationToken);
        await _compressedUrlRepository.SaveChangesAsync(cancellationToken);
    }
}