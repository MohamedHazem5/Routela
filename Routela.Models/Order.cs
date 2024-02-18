namespace Routela.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int CourseId { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public DateTime buydate { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
        public Course Course { get; set; }
    }
}
