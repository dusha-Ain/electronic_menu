namespace ElectronicMenu.Service.Settings;

public class ElectronicMenuSettingsReader {
    public static ElectronicMenuSettings Read(IConfiguration configuration)
    {
        return new ElectronicMenuSettings()
        {
             ElectronicMenuDbConnectionString =
                configuration.GetConnectionString("ElectronicMenuDbContext")
        };
    }
}