using Microsoft.EntityFrameworkCore;
using UrlCompressor.Application.Services;
using UrlCompressor.Domain;

namespace UrlCompressor.Infrastructure.Persistence;

public class CompressedUrlRepository : ICompressedUrlRepository
{
    private readonly UrlCompressorDbContext _dbContext;

    public CompressedUrlRepository(UrlCompressorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<CompressedUrl?> GetByUrlAsync(string url, CancellationToken cancellationToken)
    {
        return _dbContext.CompressedUrls.FirstOrDefaultAsync(x => x.InitialUrl == url || x.SmallUrl == url, cancellationToken);
    }

    public Task<CompressedUrl?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _dbContext.CompressedUrls.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<List<CompressedUrl>> GetAllAsync(int from, int to, CancellationToken cancellationToken)
    {
        return _dbContext.CompressedUrls.Skip(from).Take(to).ToListAsync(cancellationToken);
    }

    public Task<int> GetCountAsync(CancellationToken cancellationToken)
    {
        return _dbContext.CompressedUrls.CountAsync(cancellationToken);
    }

    public void Add(CompressedUrl url)
    {
        _dbContext.CompressedUrls.Add(url);
    }

    public void Delete(CompressedUrl url)
    {
        _dbContext.CompressedUrls.Remove(url);
    }

    public void UpdateUrl(CompressedUrl urlToUpdate)
    {
        _dbContext.Entry(urlToUpdate).State = EntityState.Modified;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}