using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UrlCompressor.Application.Common.Configuration;
using UrlCompressor.Application.Requests.Commands.CreateUrl;
using UrlCompressor.Application.Requests.Commands.DeleteUrl;
using UrlCompressor.Application.Requests.Commands.HitUrl;
using UrlCompressor.Application.Requests.Commands.UpdateUrl;
using UrlCompressor.Application.Requests.Queries.GetUrl;
using UrlCompressor.Application.Requests.Queries.GetUrls;
using UrlCompressor.Presentation.Models;

namespace UrlCompressor.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet("/links/{page}")]
    public async Task<IActionResult> ListAll(int page, [FromServices] GetUrlsHandler handler, [FromServices] IApplicationConfiguration applicationConfiguration)
    {
        return View((await handler.HandleAsync(new GetUrlsRequest(page, 10), CancellationToken.None), page));
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet("{url}")]
    public async Task<IActionResult> HitPage(string url, [FromServices] HitUrlHandler handler, [FromServices] IApplicationConfiguration applicationConfiguration)
    {
        var res = await handler.HandleAsync(new HitUrlRequest(url), default);
        return Redirect(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Index(
        string url, 
        [FromServices] CreateUrlHandler createUrlHandler, 
        CancellationToken cancellationToken)
    {
        var res = await createUrlHandler.HandleAsync(new CreateUrlRequest(url), cancellationToken);
        return RedirectToAction("Details", new {key = res.Id});
    }
    
    [HttpPost("details")]
    public async Task<IActionResult> UpdateLink(
        string key,
        string url, 
        [FromServices] UpdateUrlHandler updateUrlHandler, 
        CancellationToken cancellationToken)
    {
        await updateUrlHandler.HandleAsync(new UpdateUrlRequest(key, url), cancellationToken);
        return RedirectToAction("Details", new {key = key});
    }
    
    [HttpGet("/link/{key}")]
    public async Task<IActionResult> Details(string key, [FromServices] GetUrlHandler handler)
    {
        var res = await handler.HandleAsync(new GetUrlRequest(key), CancellationToken.None);
        return View(res);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            ExceptionMessage = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error.Message
        });
    }

    public async Task<IActionResult> Delete(string key, [FromServices] DeleteUrlHandler handler)
    {
        await handler.HandleAsync(new DeleteUrlRequest(key), CancellationToken.None);
        return RedirectToAction("ListAll", new {page = 1});
    }
}