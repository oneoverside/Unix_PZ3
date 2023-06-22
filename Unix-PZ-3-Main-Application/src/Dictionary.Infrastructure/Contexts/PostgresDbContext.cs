using Dictionary.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Infrastructure.Contexts;

internal class PostgresDbContext : DbContext
{
    public DbSet<InvenItem> Items { get; set; }
    
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InvenItem>().ToTable("inven_item");
    }
}