using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroBlog.Entities;
using MicroBlog.Repository;

namespace MicroBlog.Service
{
    public partial class MicroBloggingService : IMicroBloggingService
    {
        private readonly IUnitOfWork _microBlogUnitOfWork;

        public MicroBloggingService(IUnitOfWork microBlogUnitOfWork)
        {
            _microBlogUnitOfWork = microBlogUnitOfWork;
        }

        public IEnumerable<Post> GetRecentPosts()
        {
            var posts =_microBlogUnitOfWork.Posts.GetAll().OrderByDescending(o => o.CreatedOn);
            foreach (var post in posts)
            {
                post.User = _microBlogUnitOfWork.Users.Get(post.AuthorId);
            }

            return posts;
        }

        public async Task<IEnumerable<Post>> GetRecentPostsAsync()
        {
            var posts = await _microBlogUnitOfWork.Posts.GetAllAsync();
            return posts.OrderBy(o => o.CreatedOn);
        }

        public Post GetPostById(int id)
        {
            return _microBlogUnitOfWork.Posts.Get(id);
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _microBlogUnitOfWork.Posts.GetAsync(id);
        }

        public Post CreatePost(Post post)
        {
            post.User = _microBlogUnitOfWork.Users.FindUserByName(post.UserName);
            var retPost = _microBlogUnitOfWork.Posts.Create(post);
            _microBlogUnitOfWork.Complete();

            return retPost;
        }

        public void RemovePostById(int id)
        {
            var post = _microBlogUnitOfWork.Posts.Get(id);
            _microBlogUnitOfWork.Posts.Delete(post);
            _microBlogUnitOfWork.Complete();
        }
    }
}