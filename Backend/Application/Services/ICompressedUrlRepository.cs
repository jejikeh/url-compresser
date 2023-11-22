using Application.Requests.Commands.UpdateUrl;
using UrlCompressor.Domain;

namespace Application.Services;

public interface ICompressedUrlRepository
{
    public Task<CompressedUrl?> GetByUrlAsync(string url, CancellationToken cancellationToken);
    public Task<CompressedUrl?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<List<CompressedUrl>> GetAllAsync(int from, int to, CancellationToken cancellationToken);
    public Task AddAsync(CompressedUrl url, CancellationToken cancellationToken);
    public Task DeleteAsync(CompressedUrl url, CancellationToken cancellationToken);
    public Task UpdateUrlAsync(CompressedUrl urlToUpdate, string newUrl, string newSmallUrl, CancellationToken cancellationToken);
    public Task IncrementHitsAsync(CompressedUrl url, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}