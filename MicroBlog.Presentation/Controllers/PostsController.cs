using System;
using System.Web.Http;
using MicroBlog.Entities;
using MicroBlog.Service;

namespace MicroBlog.Presentation.Controllers
{
    [RoutePrefix("api/posts")]
    public class PostsController : ApiController
    {
        private readonly IMicroBloggingService _microBloggingService;

        public PostsController(IMicroBloggingService microBloggingService)
        {
            _microBloggingService = microBloggingService;
        }

        [HttpGet]
        [Route("recent")]
        public IHttpActionResult GetRecent()
        {
            var recentPosts = _microBloggingService.GetRecentPosts();
            if (null == recentPosts)
            {
                return NotFound();   
            }
            return Ok(recentPosts);
        }

        [HttpGet]
        [Route("{id:int}")]
        public Post GetById(int id)
        {
            return _microBloggingService.GetPostById(id);
        }

        [HttpPost]
        public IHttpActionResult CreatePost([FromBody]Post post)
        {
            if (post.CreatedOn == DateTime.MinValue)
            {
                post.CreatedOn = DateTime.UtcNow;
            }
            try
            {
                _microBloggingService.CreatePost(post);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeletePost(int id)
        {
            try
            {
                _microBloggingService.RemovePostById(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}