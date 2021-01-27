using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.Server.Data.Models;

namespace Portfolio.Server.Data
{
  public class ApplicationDbContext : IdentityDbContext<User>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }

    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {

      builder.Entity<Comment>()
             .Property(c => c.CreationTime)
             .HasDefaultValueSql("GETUTCDATE()");

      builder.Entity<Comment>()
             .HasQueryFilter(c => !c.IsDeleted);


      base.OnModelCreating(builder);
    }
  }
}
