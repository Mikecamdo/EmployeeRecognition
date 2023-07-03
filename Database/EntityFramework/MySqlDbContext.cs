using EmployeeRecognition.Database;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRecognition.Database.EntityFramework;

public class MySqlDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Kudo> Kudos { get; set; }
    public DbSet<Comment> Comments { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<Kudo>().HasKey(k => k.Id);
        modelBuilder.Entity<Comment>().HasKey(c => c.Id);
    }
    public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
    {
    }
}
