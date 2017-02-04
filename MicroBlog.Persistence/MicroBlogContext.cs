using System.Data.Entity;
using MicroBlog.Entities;

namespace MicroBlog.Persistence
{
    public class MicroBlogContext : DbContext
    {
        public MicroBlogContext()
            : base("name=MicroBlogDb")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ModelConfiguration.Configure(modelBuilder);
        }
    }
}