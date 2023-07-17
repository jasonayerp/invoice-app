using Microsoft.EntityFrameworkCore.Design;

namespace Invoice.Api.Data.Cosmos;

public sealed class CosmosDesignTimeDbContextFactory : IDesignTimeDbContextFactory<CosmosDbContext>
{
    public CosmosDbContext CreateDbContext(string[] args)
    {
        var connectionString = args[0];
        var parts = connectionString.Split(';');
        var accountEndpoint = parts[0];
        var accountKey = parts[1];
        var databaseName = parts[2];
        var optionsBuilder = new DbContextOptionsBuilder<CosmosDbContext>();
        optionsBuilder.UseCosmos(accountEndpoint, accountKey, databaseName);
        return new CosmosDbContext(optionsBuilder.Options);
    }
}
