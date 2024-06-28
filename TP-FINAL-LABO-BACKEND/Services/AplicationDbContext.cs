using Microsoft.EntityFrameworkCore;
using TP_FINAL_LABO_BACKEND.Models.Comment;
using TP_FINAL_LABO_BACKEND.Models.Like;
using TP_FINAL_LABO_BACKEND.Models.Post;
using TP_FINAL_LABO_BACKEND.Models.Role;
using TP_FINAL_LABO_BACKEND.Models.User;


namespace TP_FINAL_LABO_BACKEND.Services
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }
        public virtual DbSet<Comment> Comment { get; set; } = null!;
        public DbSet<Like> Likes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<Post>().HasIndex(t => t.Title).IsUnique();

            modelBuilder.Entity<Role>().HasData(
                new Role { IdRole = 1, NameRole = "Admin" },
                new Role { IdRole = 2, NameRole = "User" },
                new Role { IdRole = 3, NameRole = "Moderator" }
            );

            modelBuilder.Entity<User>().HasMany(u => u.Roles).WithMany().UsingEntity<RoleUsers>(
                l => l.HasOne<Role>().WithMany().HasForeignKey(e => e.RoleId),
                r => r.HasOne<User>().WithMany().HasForeignKey(e => e.UserId)
            );

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.IdComment);

                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Post)
                     .WithMany()
                     .HasForeignKey(d => d.IdPost)
                     .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.IdPost);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.CreatedPost).IsRequired();
                entity.HasMany(p => p.Comments)
                      .WithOne(c => c.Post)
                      .HasForeignKey(c => c.IdPost)
                      .OnDelete(DeleteBehavior.NoAction); // Adjust deletion behavior as needed
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.NameUser).IsRequired().HasMaxLength(128);
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.PasswordUser).IsRequired();
                entity.Property(e => e.CreatedUser).IsRequired();
                entity.Property(e => e.Birthdate).IsRequired();
            });
        }
    }
}
