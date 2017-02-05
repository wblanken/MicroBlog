using System;
using System.Collections.Generic;
using System.Linq;
using MicroBlog.Entities;
using MicroBlog.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroBlog.Service.Tests
{
    [TestClass]
    public class MicroBloggingServiceTests
    {
        private IMicroBloggingService _microBlogService;
        private Post _testPost;
        private MicroBlogContext _context;
        private UnitOfWork _microBlogUnitOfWork;
        private List<Post> _testPosts;

        [TestInitialize]
        public void TestInit()
        {
            // Normally I'd have a fake context or sepeperate db for testing but for simplicity in this example I just add and remove entries from the existing db
            _testPosts = new List<Post>();
            _context = new MicroBlogContext();

            // Create a post to use during testing.
            _testPost = _context.Posts.Add(new Post
            {
                CreatedOn = DateTime.Now,
                Message = "Test Post! This post was created in the test initialization method!",
                AuthorId = 1
            });
            _context.SaveChanges();

            _testPosts.Add(_testPost);

            _microBlogUnitOfWork = new UnitOfWork(_context);
            _microBlogService = new MicroBloggingService(_microBlogUnitOfWork);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Posts.RemoveRange(_testPosts);
            _context.SaveChanges();

            _microBlogUnitOfWork.Dispose();
            _context?.Dispose();
        }

        [TestMethod]
        public void GetRecentPostsTest()
        {
            var recentPosts =  _microBlogService.GetRecentPosts();
            Assert.IsNotNull(recentPosts);
            Assert.IsTrue(recentPosts.Contains(_testPost));
        }

        [TestMethod]
        public void GetPostByIdTest()
        {
            var post = _microBlogService.GetPostById(_testPost.Id);
            Assert.IsNotNull(post);
            Assert.AreSame(_testPost, post);
        }

        [TestMethod]
        public void CreatePostTest()
        {
            var newPost = new Post
            {
                Message = "New test posting!",
                CreatedOn = DateTime.Now,
                AuthorId = 1
            };

            var retPost = _microBlogService.CreatePost(newPost);
            _testPosts.Add(retPost);

            Assert.AreEqual(newPost, retPost);
        }

        [TestMethod]
        public void RemovePostByIdTest()
        {
            _microBlogService.RemovePostById(_testPost.Id);

            var recentPosts = _microBlogService.GetRecentPosts();
            Assert.IsFalse(recentPosts.Contains(_testPost));

            _testPosts.Remove(_testPost);
        }
    }
}