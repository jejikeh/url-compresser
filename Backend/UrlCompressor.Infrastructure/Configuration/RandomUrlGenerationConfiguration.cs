namespace UrlCompressor.Infrastructure.Configuration;

public struct RandomUrlGenerationConfiguration
{
    public enum GenerationMethod
    {
        Random,
        Base64,
    }
    
    public string Length { get; set; }
    public GenerationMethod Method { get; set; }
}