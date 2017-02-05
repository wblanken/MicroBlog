using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using MicroBlog.Entities;
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
            _mockService.Setup(s => s.RemovePostById(It.IsAny<int>())).Callback<int>(c => _mockData.Remove(_mockData.First(f => f.Id == c)));

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
            var testPost = new Post
            {
                AuthorId = 1,
                CreatedOn = DateTime.UtcNow,
                Message = "Created Post!",
                Id = 2
            };
            controller.CreatePost(Post);
            _mockService.Verify(v => v.CreatePost(testPost), Times.Once);

            var posts = controller.GetRecent() as OkNegotiatedContentResult<IEnumerable<Post>>;
            Assert.IsTrue(posts.Content.Contains(testPost));
        }

        [TestMethod]
        public void DeletePostTest()
        {
            var controller = new PostsController(_bloggingService);
            var testPost = new Post
            {
                AuthorId = 1,
                CreatedOn = DateTime.UtcNow,
                Message = "I will be deleted!",
                Id = 3
            };
            controller.CreatePost(testPost);

            var posts = controller.GetRecent() as OkNegotiatedContentResult<IEnumerable<Post>>;
            Assert.IsTrue(posts.Content.Contains(testPost));

            controller.DeletePost(3);
            posts = controller.GetRecent() as OkNegotiatedContentResult<IEnumerable<Post>>;
            Assert.IsFalse(posts.Content.Contains(testPost));
        }
    }
}