using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
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
        [Authorize]
        public IHttpActionResult CreatePost([FromBody]Post post)
        {
            // TODO: Fix user post tracking, this is a workaround for now.
            var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userName = principal.Claims.First(f => f.Type == "sub").Value;

            post.CreatedOn = DateTime.UtcNow;
            post.UserName = userName;

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