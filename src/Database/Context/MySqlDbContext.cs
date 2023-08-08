using EmployeeRecognition.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRecognition.Database.Context;

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

    private void CreateEmployeeRecognitionComponents(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Password).HasMaxLength(80).IsRequired();
            entity.Property(e => e.AvatarUrl).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Bio).HasMaxLength(500).IsRequired();

            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<Kudo>(entity =>
        {
            entity.ToTable("Kudos");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.SenderId).HasMaxLength(50);
            entity.Property(e => e.ReceiverId).HasMaxLength(50).IsRequired();
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

            entity.HasOne(e => e.Sender)
                .WithMany(s => s.KudosSent)
                .HasForeignKey(e => e.SenderId);

            entity.HasOne(e => e.Receiver)
                .WithMany(s => s.KudosReceived)
                .HasForeignKey(e => e.ReceiverId);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comments");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.KudoId).IsRequired();
            entity.Property(e => e.SenderId).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Message).HasMaxLength(200).IsRequired();

            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.Kudo)
                .WithMany(k => k.Comments)
                .HasForeignKey(e => e.KudoId);

            entity.HasOne(e => e.Sender)
                .WithMany(u => u.CommentsSent)
                .HasForeignKey(e => e.SenderId);
        });
    }
    
}
