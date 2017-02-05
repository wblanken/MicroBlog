// Copyright (c) 2017 Will Blankenship All Rights Reserved.

using System.Data.Entity;
using System.Linq;
using MicroBlog.Entities;
using MicroBlog.Repository;

namespace MicroBlog.Persistence
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {}

        public User FindUserByName(string userName)
        {
            return BlogContext.Users.SingleOrDefault(s => s.UserName == userName);
        }

        public MicroBlogContext BlogContext => this.Context as MicroBlogContext;
    }
}