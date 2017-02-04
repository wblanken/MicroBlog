using System.Data.Entity;
using MicroBlog.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MicroBlog.Persistence
{
    public class MicroBlogContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public MicroBlogContext()
            : base("name=MicroBlogDb")
        {
            Database.SetInitializer<MicroBlogContext>(null);
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static MicroBlogContext Create()
        {
            return new MicroBlogContext();
        }

        // Identity and Authorization
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ModelConfiguration.Configure(modelBuilder);
        }
    }
}