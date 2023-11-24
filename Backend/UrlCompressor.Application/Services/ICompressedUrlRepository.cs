using UrlCompressor.Application.Requests.Commands.UpdateUrl;
using UrlCompressor.Domain;

namespace UrlCompressor.Application.Services;

public interface ICompressedUrlRepository
{
    public Task<CompressedUrl?> GetByUrlAsync(string url, CancellationToken cancellationToken);
    public Task<CompressedUrl?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<List<CompressedUrl>> GetAllAsync(int from, int to, CancellationToken cancellationToken);
    public Task<int> GetCountAsync(CancellationToken cancellationToken);
    public void Add(CompressedUrl url);
    public void Delete(CompressedUrl url);
    public void UpdateUrl(CompressedUrl urlToUpdate);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}