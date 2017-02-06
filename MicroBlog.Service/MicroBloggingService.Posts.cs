using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
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

        /// <summary>
        /// Return a collection of all posts
        /// </summary>
        /// <remarks>Currently returns every post in the database, 
        /// will need chop this down if I ever care to continue work on this project</remarks>
        public IEnumerable<Post> GetRecentPosts()
        {
            var posts =_microBlogUnitOfWork.Posts.GetAll().OrderByDescending(o => o.CreatedOn);
            foreach (var post in posts)
            {
                post.User = _microBlogUnitOfWork.Users.Get(post.AuthorId);
            }

            return posts;
        }

        /// <summary>
        /// Return a collection of all posts
        /// </summary>
        /// <remarks>Currently returns every post in the database, 
        /// will need chop this down if I ever care to continue work on this project</remarks>
        public async Task<IEnumerable<Post>> GetRecentPostsAsync()
        {
            var posts = await _microBlogUnitOfWork.Posts.GetAllAsync();
            return posts.OrderBy(o => o.CreatedOn);
        }

        /// <summary>
        /// Find and return a post by Id
        /// </summary>
        /// <param name="id">Post Id</param>
        public Post GetPostById(int id)
        {
            return _microBlogUnitOfWork.Posts.Get(id);
        }

        /// <summary>
        /// Find and return a post by Id
        /// </summary>
        /// <param name="id">Post Id</param>
        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _microBlogUnitOfWork.Posts.GetAsync(id);
        }

        /// <summary>
        /// Create a new post
        /// </summary>
        /// <param name="post">the post data</param>
        /// <returns>The created post</returns>
        public Post CreatePost(Post post)
        {
            post.User = _microBlogUnitOfWork.Users.FindUserByName(post.UserName);
            post.AuthorId = post.User.Id;
            var retPost = _microBlogUnitOfWork.Posts.Create(post);
            _microBlogUnitOfWork.Complete();

            return retPost;
        }

        /// <summary>
        /// Upate an existing post
        /// </summary>
        /// <param name="message">The new post message</param>
        /// <param name="userName">UserName trying to update the post</param>
        /// <param name="postId">Id of the post being updated</param>
        /// <returns>The updated post</returns>
        public Post UpdatePost(string message, string userName, int postId)
        {
            var post = _microBlogUnitOfWork.Posts.Get(postId);

            if (!VerifyUserAllowedToChangePost(userName, post.AuthorId))
            {
                throw new InvalidCredentialException("This user is not allowed to edit another user's post!");
            }

            post.Message = message;
            var retMessage = _microBlogUnitOfWork.Posts.Update(post, postId);
            _microBlogUnitOfWork.Complete();

            return retMessage;
        }

        /// <summary>
        /// Finds and removes a post but only if the person trying to remove the post is the original user.
        /// </summary>
        /// <param name="id">Id of Post to delete</param>
        /// <param name="userName">UserName trying to delete the post</param>
        /// <exception cref="InvalidCredentialException">Thrown if a user other than the author tries to delete the post.</exception>
        public void RemovePostById(int id, string userName)
        {
            var post = _microBlogUnitOfWork.Posts.Get(id);

            if (!VerifyUserAllowedToChangePost(userName, post.AuthorId))
            {
                throw new InvalidCredentialException("This user is not allowed to remove another user's post!");
            }

            _microBlogUnitOfWork.Posts.Delete(post);
            _microBlogUnitOfWork.Complete();
        }

        /// <summary>
        /// Check if a given user name is the author of a post.
        /// </summary>
        /// <param name="userName">UserName to verify</param>
        /// <param name="postAuthorId">Post author id</param>
        /// <remarks>If I ever implement roles then I could do a role override check here.</remarks>
        /// <returns>True if the author is the same person</returns>
        private bool VerifyUserAllowedToChangePost(string userName, int postAuthorId)
        {
            var user = _microBlogUnitOfWork.Users.FindUserByName(userName);
            return user.Id == postAuthorId;
        }
    }
}