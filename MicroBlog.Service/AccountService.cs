using System.Threading.Tasks;
using MicroBlog.Entities;
using MicroBlog.Repository;
using Microsoft.AspNet.Identity;

namespace MicroBlog.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAuthRepository _authRepository;

        public AccountService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<IdentityResult> RegisterUserAsync(string userName, string password)
        {
            return await _authRepository.RegisterUser(userName, password);
        }

        public async Task<User> FindUser(string userName, string password)
        {
            return await _authRepository.FindUser(userName, password);
        }

        public void Dispose()
        {
            _authRepository.Dispose();
        }
    }
}