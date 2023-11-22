namespace UrlCompressor.Domain;

public class CompressedUrl
{
    public Guid Id { get; init; }
    
    public required string InitialUrl { get; init; }
    public required string SmallUrl { get; init; }
    
    public DateTime CreatedAt { get; init; }
    
    public int Hits { get; private set; }

    public void Hit()
    {
        Hits += 1;
    }
}