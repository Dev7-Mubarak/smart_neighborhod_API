using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SmartNeighborhoodAPI.Services.Auth;

namespace SmartNeighborhoodAPI
{
    /// <summary>
    /// EF Core design-time factory invoked by the CLI tools (add-migration, update-database, etc.).
    /// Supplies a <see cref="DesignTimeCurrentUserService"/> that acts as a SuperAdmin so that
    /// global query filters are disabled and the full schema is visible during migrations.
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(
                configuration.GetConnectionString("RemoteConnection"));

            // DesignTimeCurrentUserService bypasses all query filters so EF tools can
            // see the complete schema. Never used at runtime.
            return new ApplicationDbContext(optionsBuilder.Options, new DesignTimeCurrentUserService());
        }
    }
}
