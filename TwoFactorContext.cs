using System.Reflection;
using Microsoft.EntityFrameworkCore;
using twoFactorAuthentication.Entities;

namespace twoFactorAuthentication;

public class TwoFactorContext : DbContext
{
    public TwoFactorContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
}