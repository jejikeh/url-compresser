using Application.Common.Configuration;

namespace Application.Services;

public interface IApplicationConfiguration
{
    public ApplicationConfiguration ApplicationConfiguration { get; }
}