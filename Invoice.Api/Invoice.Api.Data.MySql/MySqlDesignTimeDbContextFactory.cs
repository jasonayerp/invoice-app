using Microsoft.EntityFrameworkCore.Design;

namespace Invoice.Api.Data.MySql;

public sealed class MySqlDesignTimeDbContextFactory : IDesignTimeDbContextFactory<MySqlDbContext>
{
    public MySqlDbContext CreateDbContext(string[] args)
    {
        var connectionString = args[0];

        var optionsBuilder = new DbContextOptionsBuilder<MySqlDbContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        return new MySqlDbContext(optionsBuilder.Options);
    }
}
