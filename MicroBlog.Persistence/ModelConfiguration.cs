using System.Data.Entity;
using MicroBlog.Entities;

namespace MicroBlog.Persistence
{
    internal class ModelConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            ConfigurePostEntity(modelBuilder);
        }

        private static void ConfigurePostEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasKey(k => k.Id);
        }
    }
}