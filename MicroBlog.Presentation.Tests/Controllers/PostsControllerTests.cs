using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using MicroBlog.Entities;
using MicroBlog.Presentation.ViewModels;
using MicroBlog.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MicroBlog.Presentation.Controllers.Tests
{
    [TestClass]
    public class PostsControllerTests
    {
        private Mock<IMicroBloggingService> _mockService;
        private IMicroBloggingService _bloggingService;
        private List<Post> _mockData;

        [TestInitialize]
        public void TestInit()
        {
            _mockData = new List<Post>
            {
                new Post {Id = 1, AuthorId = 1, CreatedOn = DateTime.UtcNow, Message = "Test Posting!"}
            };

            _mockService = new Mock<IMicroBloggingService>();
            _mockService.Setup(s => s.GetRecentPosts()).Returns(_mockData);
            _mockService.Setup(s => s.GetPostById(It.IsAny<int>())).Returns<int>(id => _mockData.SingleOrDefault(s => s.Id == id));
            _mockService.Setup(s => s.CreatePost(It.IsAny<Post>())).Callback<Post>(c => _mockData.Add(c));
            _mockService.Setup(s => s.RemovePostById(It.IsAny<int>(), It.IsAny<string>())).Callback<int, string>((c,s) => _mockData.Remove(_mockData.First(f => f.Id == c)));

            _bloggingService = _mockService.Object;
        }

        [TestMethod]
        public void GetRecentTest()
        {
            var controller = new PostsController(_bloggingService);
            var recentPosts = controller.GetRecent() as OkNegotiatedContentResult<IEnumerable<Post>>;
            Assert.IsNotNull(recentPosts);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            var controller = new PostsController(_bloggingService);
            var post = controller.GetById(1);
            Assert.IsNotNull(post);
        }

        [TestMethod]
        public void CreatePostTest()
        {
            var controller = new PostsController(_bloggingService);
            var testPost = new CreatePostViewModel
            {
                message = "Created Post!",
                userName = "testUser"
            };
            controller.CreatePost(testPost);
            
            var posts = controller.GetRecent() as OkNegotiatedContentResult<IEnumerable<Post>>;
            Assert.IsTrue(posts.Content.Select(s => s.Message).Contains(testPost.message));
        }

        // TODO: This is broken, there's not a clean way to spoof a static method using Moq  that I can find and will require more research that I currently have time for :)
        /*[TestMethod]
        public void DeletePostTest()
        {
            var controller = new PostsController(_bloggingService);

            // Mock data for finding user name
            var mockRequest = new Mock<HttpRequestMessage>();
            var claims = new List<Claim>
            {
                new Claim("sub", "testUser")
            };
            var identity = new ClaimsIdentity(claims);
            var context = new HttpRequestContext
            {
                Principal = new ClaimsPrincipal(identity)
            };
            mockRequest.Setup(s => s.GetRequestContext()).Returns(context);

            controller.Request = mockRequest.Object;

            var testPost = new Post
            {
                AuthorId = 1,
                CreatedOn = DateTime.UtcNow,
                Message = "I will be deleted!",
                Id = 33
            };
            _mockData.Add(testPost);

            controller.DeletePost(33);
            var posts = controller.GetRecent() as OkNegotiatedContentResult<IEnumerable<Post>>;
            Assert.IsFalse(posts.Content.Contains(testPost));
        }*/
    }
}