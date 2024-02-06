using UrlCompressor.Application.Common.Configuration;
using UrlCompressor.Application.Requests;
using UrlCompressor.Infrastructure.Configuration;
using UrlCompressor.Infrastructure.Persistence;

namespace UrlCompressor.Presentation.Configuration;

public static class ApplicationConfigurator
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        var applicationConfiguration = new AppConfiguration();

        builder.Services
            .AddSingleton<IApplicationConfiguration>(applicationConfiguration)
            .AddSingleton<IInfrastructureConfiguration>(applicationConfiguration)
            .AddRequestsHandlers()
            .AddInfrastructure(applicationConfiguration)
            .AddControllersWithViews();
        
        return builder;
    }
    
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        
        return app;
    }
    
    public static async Task<WebApplication> RunApplicationAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        
        try
        {
            var versityUsersDbContext = serviceProvider.GetRequiredService<UrlCompressorDbContext>();
            await versityUsersDbContext.Database.EnsureCreatedAsync();
            
            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR:" + ex.Message);
            
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Host terminated unexpectedly");
        }

        return app;
    }
}