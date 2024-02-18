using System.ComponentModel.DataAnnotations.Schema;

namespace Routela.Models
{
    public class UserReview
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public double Rate { get; set; }
        public string Comment { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}