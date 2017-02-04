using System.ComponentModel.DataAnnotations;

namespace MicroBlog.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
