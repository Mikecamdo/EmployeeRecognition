using EmployeeRecognition.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRecognition.Database.EntityFramework;

public class MySqlDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Kudo> Kudos { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options) { }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CreateEmployeeRecognitionComponents(modelBuilder);
    }

    //FIXME eventually need to update this and the database to use foreign keys
    private void CreateEmployeeRecognitionComponents(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Department).HasMaxLength(50).IsRequired();
            entity.Property(e => e.AvatarUrl).HasMaxLength(50).IsRequired();

            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<Kudo>(entity =>
        {
            entity.ToTable("kudos");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Sender).HasMaxLength(50).IsRequired();
            entity.Property(e => e.SenderId).HasMaxLength(50).IsRequired();
            entity.Property(e => e.SenderAvatar).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Receiver).HasMaxLength(50).IsRequired();
            entity.Property(e => e.ReceiverId).HasMaxLength(50).IsRequired();
            entity.Property(e => e.ReceiverAvatar).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Title).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Message).HasMaxLength(200).IsRequired();
            entity.Property(e => e.TeamPlayer).IsRequired();
            entity.Property(e => e.OneOfAKind).IsRequired();
            entity.Property(e => e.Creative).IsRequired();
            entity.Property(e => e.HighEnergy).IsRequired();
            entity.Property(e => e.Awesome).IsRequired();
            entity.Property(e => e.Achiever).IsRequired();
            entity.Property(e => e.Sweetness).IsRequired();
            entity.Property(e => e.TheDate).IsRequired();

            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("comments");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.KudosId).IsRequired();
            entity.Property(e => e.SenderName).HasMaxLength(50).IsRequired();
            entity.Property(e => e.SenderId).HasMaxLength(50).IsRequired();
            entity.Property(e => e.SenderAvatar).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Message).HasMaxLength(200).IsRequired();

            entity.HasKey(e => e.Id);
        });
    }
    
}
