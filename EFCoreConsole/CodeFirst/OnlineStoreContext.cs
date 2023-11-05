
using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst;

/// <summary>
/// Add-Migration <migration comment> | "migration comment" // to add migration.
/// Update-Database // to add the migration to database.
/// </summary>
public class OnlineStoreContext : DbContext
{
    public DbSet<Customer> Customers { get; set; } = null;
    public DbSet<Product> Products { get; set; } = null;

    //just because we are using console app without DI so override this and provide the connectionString
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Store;Integrated Security=True;");
    }
}
