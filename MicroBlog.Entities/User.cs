// Copyright (c) 2017 Will Blankenship All Rights Reserved.

using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MicroBlog.Entities
{
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim> 
    {
        public ICollection<Post> Posts { get; set; }
        public ICollection<Post> RePosts { get; set; }
    }
}