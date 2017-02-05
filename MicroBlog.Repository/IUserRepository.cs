// Copyright (c) 2017 Will Blankenship All Rights Reserved.

using MicroBlog.Entities;

namespace MicroBlog.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User FindUserByName(string userName);
    }
}