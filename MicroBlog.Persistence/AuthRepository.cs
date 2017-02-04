using System.Threading.Tasks;
using MicroBlog.Entities;
using MicroBlog.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MicroBlog.Persistence
{
    public class AuthRepository : IAuthRepository
    {
        private readonly MicroBlogContext _ctx;
        private readonly UserManager<User, int> _userManager;

        public AuthRepository()
        {
            _ctx = new MicroBlogContext();
            _userManager = new UserManager<User, int>(new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(string userName, string password)
        {
            var user = new User
            {
                UserName = userName
            };

            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

       public async Task<User> FindUser(string userName, string password)
       {
           return await _userManager.FindAsync(userName, password);
       }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }
    }
}
