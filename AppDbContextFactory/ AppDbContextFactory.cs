using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        var connectionString = "Server=localhost;Database=curso;User=root;Password=fuba2018";

        optionsBuilder.UseMySql(connectionString, ServerVersion.Create(new Version(8, 0, 36), ServerType.MySql));
        return new AppDbContext(optionsBuilder.Options);
    }
}
