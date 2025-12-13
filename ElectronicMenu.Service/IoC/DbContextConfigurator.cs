using ElectronicMenu.Service.Settings;
using ElectronicMenuDataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace ElectronicMenu.Service.IoC;

public static class DbContextConfigurator
{
    public static void ConfigureService(IServiceCollection services,  ElectronicMenuSettings settings)
    {
        services.AddDbContextFactory< ElectronicMenuDbContext>(options =>
        {
            options.UseNpgsql(settings.ElectronicMenuDbConnectionString);
        }, ServiceLifetime.Scoped);

    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory< ElectronicMenuDbContext>>();
        using var context = contextFactory.CreateDbContext();
        context.Database.Migrate();
    }
}