﻿using System;
using System.Threading.Tasks;
using MicroBlog.Entities;

namespace MicroBlog.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Post> Posts { get; }
        IUserRepository Users { get; }
        
        int Complete();
        Task<int> CompleteAsync();
    }
}
