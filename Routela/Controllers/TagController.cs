
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Routela.DataAccess.Repository.IRepository;
using Routela.Models;
using Routela.Models.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Routela.Controllers
{

    public class TagController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;


        public TagController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var tags = await _unitOfWork.Tag.GetAllAsync();
            return Ok(tags);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tag = await _unitOfWork.Tag.FirstOrDefaultAsync(d => d.Id == id);
            if (tag == null)
                return NotFound();
            return Ok(tag);
        }

        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var tag = await _unitOfWork.Tag.GetAllAsync(p => p.Name.Contains(name));
            if (tag == null)
                return NotFound();
            return Ok(tag);
        }

        [HttpPut("Edit/{id:int}")]
        public async Task<ActionResult> UpdateTagAsync(int id, [FromForm] TagDTO updatedTagDto)
        {

            // Validate 
            if (id <= 0 || !ModelState.IsValid)
                return BadRequest();

            try
            {
                // Get old category data
                var oldTag = await _unitOfWork.Tag.FirstOrDefaultAsync(c => c.Id == id);
                if (oldTag == null)
                    return NotFound();

                oldTag.Name = updatedTagDto.Name;

                _unitOfWork.Tag.Update(oldTag);
                await _unitOfWork.Save();

                // Return response
                return Ok(new
                {
                    Name = oldTag.Name,
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpDelete("Delete/{id:int}")]
        public async Task<ActionResult> DeleteTagAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid id");

                // Get category
                var tag = await _unitOfWork.Tag.FirstOrDefaultAsync(c => c.Id == id);
                if (tag == null)
                    return NotFound();

                _unitOfWork.Tag.Delete(tag);
                await _unitOfWork.Save();

                return Ok("Tag deleted");

            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetCourse/{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var coursesId = await _unitOfWork.TagCourse.GetAllAsync(f => f.TagId == id);
            if (!coursesId.Any())
                return NotFound();
            List<Course> courses = new();
            foreach (var course in coursesId)
            {
                var obj = await _unitOfWork.Course.FirstOrDefaultAsync(f => f.Id == course.CourseId);
                courses.Add(obj);
            }
            if (courses.Any())
                return NotFound();

            return Ok(courses);
        }

        [HttpGet("GetBlog/{id}")]
        public async Task<IActionResult> GetBlog(int id)
        {
            var blogId = await _unitOfWork.TagBlog.GetAllAsync(f => f.TagId == id);
            if (!blogId.Any())
                return NotFound();
            List<Blog> blogs = new();
            foreach (var blog in blogId)
            {
                var obj = await _unitOfWork.Blog.FirstOrDefaultAsync(f => f.Id == blog.BlogId);
                blogs.Add(obj);
            }
            if (blogs.Any())
                return NotFound();

            return Ok(blogs);
        }
    }
}