 using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using Configuration;
using Configuration.Extensions;
using Configuration.Options;

namespace TravelApp.Models.Extensions;

public static class DbContextDesignTimeExtensions
{
    public static DbContextOptionsBuilder ConfigureForDesignTime(
        this DbContextOptionsBuilder optionsBuilder,
        Func<DbContextOptionsBuilder, string, DbContextOptionsBuilder> databaseOptions)
    {
        Console.WriteLine("Executing DesignTimeConfigure...");

        var (configuration, databaseConnections) = CreateDesignTimeServices();
        var connection = GetDatabaseConnection(configuration, databaseConnections);

        optionsBuilder = databaseOptions(optionsBuilder, connection.DbConnectionString);

        Console.WriteLine("DesignTimeConfigure completed successfully");
        Console.WriteLine($"   User: {connection.DbUserLogin}");
        Console.WriteLine($"   Database connection: {connection.DbConnection}");

        return optionsBuilder;
    }

    private static (IConfiguration configuration, DatabaseConnections databaseConnections) CreateDesignTimeServices()
    {
        // Program.cs has not run vid EFC design-time, så vi bygger konfiguration och services manuellt här.

        var appsettingsFolder = Environment.GetEnvironmentVariable("EFC_AppSettingsFolder") ?? Directory.GetCurrentDirectory();
        Console.WriteLine($"   using appsettings.json in folder: {appsettingsFolder}");

        var appsettingsPath = Path.Combine(appsettingsFolder, "appsettings.json");
        if (!File.Exists(appsettingsPath))
            throw new FileNotFoundException($"Error: appsettings.json not found in folder: {appsettingsFolder}");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(appsettingsFolder)
            .AddJsonFile("appsettings.json", optional: false)
            .AddUserSecrets(System.Reflection.Assembly.Load("Configuration"))
            .Build();

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddOptions();
        serviceCollection.AddDatabaseConnections(configuration);
        serviceCollection.AddSingleton<IConfiguration>(configuration);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var configurationService = serviceProvider.GetRequiredService<IConfiguration>();

        

        var databaseConnections = serviceProvider.GetRequiredService<DatabaseConnections>();

        Console.WriteLine($"   DataConnectionTag: {databaseConnections.SetupInfo.DataConnectionTag}");

        return (configurationService, databaseConnections);
    }

    private static DbConnectionDetailOptions GetDatabaseConnection(IConfiguration configuration, DatabaseConnections databaseConnections)
    {
        var connection = databaseConnections.GetDataConnectionDetails(configuration["DatabaseConnections:MigrationUser"]);
        if (connection.DbConnectionString == null)
            throw new InvalidDataException($"Error: Connection string for {connection.DbConnection}, {connection.DbUserLogin} not set");

        return connection;
    }
}