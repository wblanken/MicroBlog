using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroBlog.Entities
{
    [Table("Post")]
    public class Post : BaseEntity
    {
        [Required]
        [MaxLength(140)]
        public string Message { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public User Author { get; set; }

        public ICollection<User> RePosts { get; set; }

        public override bool Equals(object obj)
        {
            var otherPost = obj as Post;

            return otherPost != null 
                && (otherPost.CreatedOn == this.CreatedOn) 
                && otherPost.Message.Equals(this.Message);
        }
    }
}
