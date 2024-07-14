using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Form> Forms { get; set; }
    public DbSet<Field> Field { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}
