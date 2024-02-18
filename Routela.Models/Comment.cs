using System.ComponentModel.DataAnnotations.Schema;

namespace Routela.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}