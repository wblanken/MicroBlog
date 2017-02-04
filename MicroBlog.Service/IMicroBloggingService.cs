using System.Collections.Generic;
using System.Threading.Tasks;
using MicroBlog.Entities;

namespace MicroBlog.Service
{
    public interface IMicroBloggingService
    {
        IEnumerable<Post> GetRecentPosts();
        Task<IEnumerable<Post>> GetRecentPostsAsync();
        Post GetPostById(int id);
        Task<Post> GetPostByIdAsync(int id);
        Post CreatePost(Post post);
        void RemovePostById(int id);
    }
}
