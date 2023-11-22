using Application.Common;
using Application.Common.Configuration;
using Application.Services;

namespace Application.Requests.Commands.UpdateUrl;

public class UpdateUrlHandler
{
    private readonly ICompressedUrlRepository _compressedUrlRepository;
    private readonly IUrlCompressorService _compressorService;
    private readonly ApplicationConfiguration _applicationConfiguration;

    public UpdateUrlHandler(ICompressedUrlRepository compressedUrlRepository, IApplicationConfiguration applicationConfiguration, IUrlCompressorService compressorService)
    {
        _compressedUrlRepository = compressedUrlRepository;
        _compressorService = compressorService;
        _applicationConfiguration = applicationConfiguration.ApplicationConfiguration;
    }

    public async Task HandleAsync(UpdateUrlRequest request, CancellationToken cancellationToken)
    {
        var url = await _compressedUrlRepository.FindCompressedUrlByIdOrUrlAsync(request.Key, cancellationToken, _applicationConfiguration);
        if (string.CompareOrdinal(request.NewInitialUrl, url.InitialUrl) == 0)
        {
            throw new HttpException(409, "Url the same!");
        }
        
        await _compressedUrlRepository.UpdateUrlAsync(
            url, 
            request.NewInitialUrl, 
            _applicationConfiguration.ChangeSmallUrlOnUpdate 
                ? _compressorService.CompressUrl(request.NewInitialUrl)
                : url.SmallUrl, 
            cancellationToken);
        
        await _compressedUrlRepository.SaveChangesAsync(cancellationToken);
    }
}