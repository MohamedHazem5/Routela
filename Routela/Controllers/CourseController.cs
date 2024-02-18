using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Routela.DataAccess.Repository.IRepository;
using Routela.Models;
using Routela.Models.DTO;
using Routela.Services.IServices;
using Stripe;

namespace Routela.Controllers
{
    public class CourseController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CourseController> _logger;


        public CourseController(IUnitOfWork unitOfWork, ILogger<CourseController> logger,
            IImageService imageService, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _imageService = imageService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var courses = await _unitOfWork.Course.GetAllAsync(null, c => c.Category, i=>i.Image);

                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Courses");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("Details/{id:int}")]
        public async Task<ActionResult> GetCourseDetailsAsync(int id)
        {
            try
            {
                var course = await _unitOfWork.Course.FirstOrDefaultAsync(c => c.Id == id, c => c.Category, i => i.Image);

                if (course == null)
                    return NotFound();

                var courseDto = new CourseDto
                {
                    Title = course.Title,
                    Description = course.Description,
                    Cost = course.Cost,
                    Duration = course.Duration,
                    CategoryId = course.CategoryId,
                    Language = course.Language,
                    SkillLevel = course.SkillLevel,
                    

                };
                return Ok(courseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting Course {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateCourse([FromForm] CourseDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var imageUploadResult = await _imageService.AddPhotoAsync(courseDto.formFile);

                var course = new Course
                {
                    Title = courseDto.Title,
                    Description = courseDto.Description,
                    CategoryId = courseDto.CategoryId,
                    SkillLevel = courseDto.SkillLevel,
                    Language = courseDto.Language,
                    Duration = courseDto.Duration,
                    Cost = courseDto.Cost,
                    InitializDate = DateTime.Now,
                    Image = new Image
                    {
                        PublicId = imageUploadResult.PublicId,
                        Url = imageUploadResult.Url.ToString(),
                    }
                };

                await _unitOfWork.Course.AddAsync(course);
                await _unitOfWork.Save();

                return Ok(new
                {
                    id = course.Id,
                    Name = course.Title,
                    Description = course.Description,
                    Cost = course.Cost,
                    TagId = course.CategoryId,
                    ImageUrl = course.Image.Url,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Course");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromForm] CourseDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var course = await _unitOfWork.Course.FirstOrDefaultAsync(p => p.Id == id);

                if (course == null)
                {
                    return NotFound();
                }

                var imageUploadResult = await _imageService.AddPhotoAsync(courseDto.formFile);

                course.Title = courseDto.Title;
                course.Description = courseDto.Description;
                course.CategoryId = courseDto.CategoryId;
                course.Duration = courseDto.Duration;
                course.Cost = courseDto.Cost;
                course.SkillLevel = courseDto.SkillLevel;
                course.Language = courseDto.Language;
                course.Image.PublicId = imageUploadResult.PublicId;
                course.Image.Url = imageUploadResult.Url.ToString();

                _unitOfWork.Course.Update(course);
                await _unitOfWork.Save();

                return Ok(course);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating course {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _unitOfWork.Course.FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                {
                    return NotFound();
                }

                _unitOfWork.Course.Delete(product);
                await _unitOfWork.Save();

                return Ok("The Course Have been Deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting course {id}");
                return StatusCode(500, "Internal server error");
            }
        }

/*
        [HttpPost]
        public async Task<IActionResult> AddCourseToUser(CourseDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCourse = _mapper.Map<Course>(CourseDto);
            newCourse.UserId = 1;
            await _unitOfWork.Course.AddAsync(newCourse);
            List<TagCourse> tags = new List<TagCourse>();
            _unitOfWork.Save();
            if (courseDto.Tags != null)
            {
                foreach (var tag in CourseD)
                {
                    tags.Add(new TagCourse
                    {
                        CourseId = newCourse.Id,
                        TagId = tag
                    });
                }
            }
            _unitOfWork.TagCourse.AddRangeAsync(tags);
            _unitOfWork.Save();

            return Ok(newCourse);
        }*/
        [HttpPost("/reviews/{courseId}")]
        public async Task<IActionResult> AddReview(int courseId, UserReview review)
        {
            var course = await _unitOfWork.Course.FirstOrDefaultAsync(c => c.Id == courseId);
            if (course == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            var newReview = new UserReview
            {
                Rate = review.Rate,
                Comment = review.Comment,
                CourseId = courseId,
                UserId = user.Id
            };

            await _unitOfWork.Review.AddAsync(newReview);
            await _unitOfWork.Save();

            return Ok(newReview);
        }

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview(int reviewId,UserReview updatedReview)
        {
            var review = await _unitOfWork.Review.FirstOrDefaultAsync(r => r.Id == reviewId);
            if (review == null)
            {
                return NotFound();
            }

            review.Rate = updatedReview.Rate;
            review.Comment = updatedReview.Comment;

            _unitOfWork.Review.Update(review);
            await _unitOfWork.Save();

            return Ok(review);
        }

        // Lecture PART //

        [HttpGet("/lectures/{id}")]
        public async Task<IActionResult> GetCourseLectures(int id)
        {
            var lectures = await _unitOfWork.Lecture.GetAllAsync(l => l.CourseId == id, l => l.Course);
            return Ok(lectures);
        }

        [HttpPost("/lectures/{id}")]
        public async Task<IActionResult> AddLecture(Lecture lecture, int courseId)
        {
            var course = await _unitOfWork.Course.FirstOrDefaultAsync(c => c.Id == courseId);
            if (course == null)
                return BadRequest($"Course with ID {courseId} not found.");

            lecture.Course = course;
            await _unitOfWork.Lecture.AddAsync(lecture);
            await _unitOfWork.Save();
            return Ok(lecture);
        }

        //POST
        [HttpPut("/lectures/{id}")]
        public async Task<IActionResult> Update(int id, Lecture updatedLecture)
        {
            var existingLecture = await _unitOfWork.Lecture.FirstOrDefaultAsync(l => l.Id == id, l => l.Course);
            if (existingLecture == null)
            {
                return NotFound();
            }

            existingLecture.Title = updatedLecture.Title;
            existingLecture.Description = updatedLecture.Description;
            existingLecture.VideoId = updatedLecture.VideoId;
            existingLecture.VideoURL = updatedLecture.VideoURL;
            existingLecture.InitializDate = updatedLecture.InitializDate;
            existingLecture.CourseId = updatedLecture.CourseId;

            await _unitOfWork.Save();

            return Ok(existingLecture);
        }

        [HttpDelete("/lectures/{id}")]
        public async Task<IActionResult> DeleteLecture(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var lecture = await _unitOfWork.Lecture.FirstOrDefaultAsync(c => c.Id == id);

            if (lecture == null)
            {
                return NotFound();
            }

            _unitOfWork.Lecture.Delete(lecture);
            await _unitOfWork.Save();

            return Ok(lecture);
        }
    }
}