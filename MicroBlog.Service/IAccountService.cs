// Copyright (c) 2017 Will Blankenship All Rights Reserved.

using System;
using System.Threading.Tasks;
using MicroBlog.Entities;
using Microsoft.AspNet.Identity;

namespace MicroBlog.Service
{
    public interface IAccountService : IDisposable
    {
        Task<IdentityResult> RegisterUserAsync(string userName, string password);
    }
}