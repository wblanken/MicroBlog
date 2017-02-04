// Copyright (c) 2017 Will Blankenship All Rights Reserved.

using System.Collections.Generic;

namespace MicroBlog.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Post> RePosts { get; set; }
    }
}