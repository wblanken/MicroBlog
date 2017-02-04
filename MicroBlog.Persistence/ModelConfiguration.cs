using System.Data.Entity;
using MicroBlog.Entities;

namespace MicroBlog.Persistence
{
    internal class ModelConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
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
                    m.MapLeftKey("UserId");
                    m.MapRightKey("PostId");
                    m.ToTable("RePost");
                });
        }

        private static void ConfigureUserEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(k => k.Id)
                .HasMany(m => m.Posts)
                .WithRequired(r => r.Author)
                .HasForeignKey(f => f.AuthorId)
                .WillCascadeOnDelete(false);
        }
    }
}