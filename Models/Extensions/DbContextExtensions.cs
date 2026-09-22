using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Configuration;

namespace TravelApp.Models.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddTravelAppDbContext(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<TravelAppDbContext>((serviceProvider, options) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var databaseConnections = serviceProvider.GetRequiredService<DatabaseConnections>();

            var defaultUser = configuration["DatabaseConnections:DefaultDataUser"];
            var conn = databaseConnections.GetDataConnectionDetails(defaultUser);

            if (databaseConnections.SetupInfo.DataConnectionServer == DatabaseServer.SQLServer)
            {
                options.UseSqlServer(conn.DbConnectionString, sql => sql.EnableRetryOnFailure());
            }
            else
            {
                throw new InvalidDataException($"DbContext for {databaseConnections.SetupInfo.DataConnectionServer} not implemented in TravelApp");
            }
        });

        return serviceCollection;
    }
}