using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MicroBlog.Entities;

namespace MicroBlog.Persistence
{
    internal class ModelConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            ConfigureIdentity(modelBuilder);
            ConfigurePostEntity(modelBuilder);
            ConfigureUserEntity(modelBuilder);
        }

        private static void ConfigurePostEntity(DbModelBuilder modelBuilder)
        {
            // This is a little contrived, there are better ways to list repostings 
            // but the assignment wanted to see a many to many relationship
            modelBuilder.Entity<Post>()
                .HasKey(k => k.Id)
                .HasMany(m => m.RePosts)
                .WithMany(m => m.RePosts)
                .Map(m =>
                {
                    m.MapLeftKey("AuthorId");
                    m.MapRightKey("PostId");
                    m.ToTable("RePost");
                });
        }

        private static void ConfigureUserEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().Property(p => p.PasswordHash).HasMaxLength(500);
            modelBuilder.Entity<User>().Property(p => p.SecurityStamp).HasMaxLength(500);
            modelBuilder.Entity<User>().Property(p => p.PhoneNumber).HasMaxLength(50);

            modelBuilder.Entity<User>()
                .HasMany(m => m.Posts)
                .WithRequired(r => r.User)
                .HasForeignKey(f => f.AuthorId)
                .WillCascadeOnDelete(false);
        }

        private static void ConfigureIdentity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<UserRole>().ToTable("UserRole").HasKey(k => k.RoleId);
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin").HasKey(k => k.UserId);
            modelBuilder.Entity<UserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<UserClaim>().Property(p => p.ClaimType).HasMaxLength(150);
            modelBuilder.Entity<UserClaim>().Property(p => p.ClaimValue).HasMaxLength(500);
        }
    }
}