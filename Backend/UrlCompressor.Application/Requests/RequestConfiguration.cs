using Microsoft.Extensions.DependencyInjection;
using UrlCompressor.Application.Requests.Commands.CreateUrl;
using UrlCompressor.Application.Requests.Commands.DeleteUrl;
using UrlCompressor.Application.Requests.Commands.HitUrl;
using UrlCompressor.Application.Requests.Commands.UpdateUrl;
using UrlCompressor.Application.Requests.Queries.GetUrl;
using UrlCompressor.Application.Requests.Queries.GetUrls;

namespace UrlCompressor.Application.Requests;

public static class RequestConfiguration
{
    public static IServiceCollection AddRequestsHandlers(this IServiceCollection services)
    {
        return services
            .AddScoped<GetUrlHandler>()
            .AddScoped<GetUrlsHandler>()
            .AddScoped<CreateUrlHandler>()
            .AddScoped<DeleteUrlHandler>()
            .AddScoped<HitUrlHandler>()
            .AddScoped<UpdateUrlHandler>();
    }
}