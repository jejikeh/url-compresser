using Microsoft.AspNetCore.Mvc;
using UrlCompressor.Application.Requests.Commands.CreateUrl;
using UrlCompressor.Application.Requests.Commands.DeleteUrl;
using UrlCompressor.Application.Requests.Commands.HitUrl;
using UrlCompressor.Application.Requests.Queries.GetUrl;
using UrlCompressor.Application.Requests.Queries.GetUrls;
using UrlCompressor.Presentation.Configuration;

var builder = WebApplication.CreateBuilder(args).ConfigureBuilder();

var app = builder.Build().ConfigureApplication();

app.MapGet("/api/urls/{key}", async (string key, [FromServices] GetUrlHandler getUrlHandler, CancellationToken cancellationToken) =>
{
    var request = new GetUrlRequest(key);
    var response = await getUrlHandler.HandleAsync(request, cancellationToken);
    return Results.Ok(response);
});

app.MapGet("/api/urls/{page}/{count}", async (int page, int count, [FromServices] GetUrlsHandler getUrlsHandler, CancellationToken cancellationToken) =>
{
    var request = new GetUrlsRequest(page, count);
    var response = await getUrlsHandler.HandleAsync(request, cancellationToken);
    return Results.Ok(response);
});

app.MapPost("/api/urls", async (CreateUrlRequest request, [FromServices] CreateUrlHandler createUrlHandler, CancellationToken cancellationToken) =>
{
    var u = await createUrlHandler.HandleAsync(request, cancellationToken);
    return Results.Created("/api/urls/" + u.Id, u);
});

app.MapDelete("/api/urls/{key}", async (string key, [FromServices] DeleteUrlHandler deleteUrlHandler, CancellationToken cancellationToken) =>
{
    var request = new DeleteUrlRequest(key);
    await deleteUrlHandler.HandleAsync(request, cancellationToken);
    return Results.NoContent();
});

app.MapPost("/api/{key}", async (string key, [FromServices] HitUrlHandler hitUrlHandler, CancellationToken cancellationToken) =>
{
    var request = new HitUrlRequest(key);
    var response = await hitUrlHandler.HandleAsync(request, cancellationToken);
    return Results.Ok(response);
});

await app.RunApplicationAsync();