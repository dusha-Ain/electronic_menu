using ElectronicMenu.Service.IoC;
using ElectronicMenu.Service.Settings;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    .Build();

var settings = ElectronicMenuSettingsReader.Read(configuration);

var builder = WebApplication.CreateBuilder(args);

DbContextConfigurator.ConfigureService(builder.Services, settings);
SerilogConfigurator.ConfigureServices(builder);
SwaggerConfigurator.ConfigureServices(builder.Services);

var app = builder.Build();

DbContextConfigurator.ConfigureApplication(app);
SerilogConfigurator.ConfigureApplication(app);
SwaggerConfigurator.ConfigureApplication(app);

app.UseHttpsRedirection();

app.Run();