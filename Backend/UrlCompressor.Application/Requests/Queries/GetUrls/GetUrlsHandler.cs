using UrlCompressor.Application.Services;
using UrlCompressor.Domain;

namespace UrlCompressor.Application.Requests.Queries.GetUrls;

public class GetUrlsHandler(ICompressedUrlRepository compressedUrlRepository)
{
    public Task<List<CompressedUrl>> HandleAsync(GetUrlsRequest request, CancellationToken cancellationToken)
    {
        return compressedUrlRepository.GetAllAsync((request.Page - 1) * request.PageSize, request.PageSize, cancellationToken);
    }
}