using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Invoice.Api.Data.SqlServer;

public class SqlServerDesignTimeDbContextFactory : IDesignTimeDbContextFactory<SqlServerDbContext>
{
    public SqlServerDbContext CreateDbContext(string[] args)
    {
        var connectionString = args[0];

        var optionsBuilder = new DbContextOptionsBuilder<SqlServerDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new SqlServerDbContext(optionsBuilder.Options);
    }
}
