using System;

using System.Threading.Tasks;
using MicroBlog.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MicroBlog.Repository
{
    public interface IAuthRepository : IDisposable
    {
        Task<IdentityResult> RegisterUser(string userName, string password);
        Task<User> FindUser(string userName, string password);
    }
}