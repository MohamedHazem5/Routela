namespace Routela.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime InitializDate { get; set; }
        public string VideoURL { get; set; }
        public string VideoId { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}