using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using MicroBlog.Entities;
using MicroBlog.Presentation.ViewModels;
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
        [Route("getPost/{id:int}")]
        public Post GetById(int id)
        {
            return _microBloggingService.GetPostById(id);
        }

        [HttpPost]
        [Authorize]
        [Route("create")]
        public IHttpActionResult CreatePost([FromBody] CreatePostViewModel postModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = new Post
            {
                Message = postModel.message,
                UserName = postModel.userName,
                CreatedOn = DateTime.UtcNow
            };

            try
            {
                if (string.IsNullOrEmpty(post.UserName))
                {
                    var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                    var userName = principal.Claims.First(f => f.Type == "sub").Value;

                    post.UserName = userName;
                }

                _microBloggingService.CreatePost(post);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("update")]
        public IHttpActionResult UpdatePost([FromBody] UpdatePostViewModel updatePost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var userName = principal.Claims.First(f => f.Type == "sub").Value;

                _microBloggingService.UpdatePost(updatePost.Message, userName, updatePost.PostId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("deletePost/{id:int}")]
        public IHttpActionResult DeletePost(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var userName = principal.Claims.First(f => f.Type == "sub").Value;

                _microBloggingService.RemovePostById(id, userName);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}