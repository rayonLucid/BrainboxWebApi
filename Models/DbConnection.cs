using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainboxWebApi.Models
{
  public class DbConnection: DbContext
  { 
    public DbConnection()
    {
    }

    public DbConnection(DbContextOptions<DbConnection> options)
        : base(options)
    {
      Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<ProductCart>()
              .Property(u => u.Amount)
              .HasComputedColumnSql("[ProductQty] + [ProductPrice]");
      base.OnModelCreating(modelBuilder);
    }
    public virtual DbSet<ProductInformation> Product { get; set; }
    public virtual DbSet<ProductCart> Cart { get; set; }
  }
}
