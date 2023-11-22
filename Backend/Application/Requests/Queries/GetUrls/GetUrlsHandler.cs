using Application.Services;
using UrlCompressor.Domain;

namespace Application.Requests.Queries.GetUrls;

public class GetUrlsHandler
{
    private readonly ICompressedUrlRepository _compressedUrlRepository;

    public GetUrlsHandler(ICompressedUrlRepository compressedUrlRepository)
    {
        _compressedUrlRepository = compressedUrlRepository;
    }

    public Task<List<CompressedUrl>> HandleAsync(GetUrlsRequest request, CancellationToken cancellationToken)
    {
        return _compressedUrlRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);
    }
}