using System.Collections.Generic;
using System.Threading.Tasks;
using MicroBlog.Entities;

namespace MicroBlog.Service
{
    public interface IMicroBloggingService
    {
        /// <summary>
        /// Return a collection of all posts
        /// </summary>
        IEnumerable<Post> GetRecentPosts();
        
        /// <summary>
        /// Return a collection of all posts
        /// </summary>
        Task<IEnumerable<Post>> GetRecentPostsAsync();

        /// <summary>
        /// Find and return a post by Id
        /// </summary>
        /// <param name="id">Post Id</param>
        Post GetPostById(int id);

        /// <summary>
        /// Find and return a post by Id
        /// </summary>
        /// <param name="id">Post Id</param>
        Task<Post> GetPostByIdAsync(int id);

        /// <summary>
        /// Create a new post
        /// </summary>
        /// <param name="post">the post data</param>
        /// <returns>The created post</returns>
        Post CreatePost(Post post);

        /// <summary>
        /// Upate an existing post
        /// </summary>
        /// <param name="message">The new post message</param>
        /// <param name="userName">UserName trying to update the post</param>
        /// <param name="postId">Id of the post being updated</param>
        /// <returns>The updated post</returns>
        Post UpdatePost(string message, string userName, int postId);

        /// <summary>
        /// Finds and removes a post but only if the person trying to remove the post is the original user.
        /// </summary>
        /// <param name="id">Id of Post to delete</param>
        /// <param name="userName">UserName trying to delete the post</param>
        void RemovePostById(int id, string userName);
    }
}
