using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PMS.Infrastructure.Data;

namespace PMS.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=.\\SQLEXPRESS;Database=ProjectManagementSystemDb;Trusted_Connection=True;TrustServerCertificate=True;"
        );

        return new AppDbContext(optionsBuilder.Options);
    }
}