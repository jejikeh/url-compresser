using UrlCompressor.Application.Common;

namespace UrlCompressor.Infrastructure.Common;

public static class Helpers
{
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
    
    public static string GenerateStringByRandom(int size)
    {
        return new string(Enumerable.Repeat(Alphabet, size).Select(x => x[Random.Shared.Next(x.Length)]).ToArray());
    }
}