using Microsoft.AspNetCore.Http;
using Routela.Models.Enums;
namespace Routela.Models.DTO
{
    public class CourseDto
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public IFormFile formFile { get; set; }
        public double Duration { get; set; }
        public SkillLevel SkillLevel { get; set; }
        public Language Language { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
