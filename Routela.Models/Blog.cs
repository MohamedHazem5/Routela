using Routela.Models.Enums;

namespace Routela.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime InitializDate { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
        public Language Language { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}