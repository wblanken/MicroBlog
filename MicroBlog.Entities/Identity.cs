using Microsoft.AspNet.Identity.EntityFramework;

namespace MicroBlog.Entities
{
    public class Role : IdentityRole<int, UserRole> { }

    public class UserLogin : IdentityUserLogin<int> { }

    public class UserRole : IdentityUserRole<int> { }
    public class UserClaim : IdentityUserClaim<int> { }
}
