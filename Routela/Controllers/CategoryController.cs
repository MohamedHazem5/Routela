using Microsoft.AspNetCore.Mvc;
using Routela.DataAccess;
using Routela.DataAccess.Repository.IRepository;
using Routela.Models;
using Routela.Models.DTO;
using Routela.Services.IServices;

namespace Routela.Controllers
{

    public class CategoryController : BaseApiController
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ApplicationDbContext _dbContext;
		private readonly ILogger<CategoryController> _logger;

		public CategoryController(IUnitOfWork unitOfWork, IImageService imageService,
		ApplicationDbContext dbContext,
		ILogger<CategoryController> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
			_dbContext = dbContext;


		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesAsync()
		{
			try
			{
				var categories = await _unitOfWork.Category.GetAllAsync();

				return Ok(categories);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting categories");
				return StatusCode(500, "Internal server error");
			}
		}

        [HttpGet("Details/{id:int}")]
		public async Task<ActionResult<Category>> GetCategoryDetailsAsync(int id)
		{
			// Validate input
			if (id <= 0)
				return BadRequest("Invalid category id");

			try
			{
				// Get category data
				var category = await _unitOfWork.Category.FirstOrDefaultAsync(c => c.Id == id);

				if (category == null)
					return NotFound();

				return Ok(category);

			}
			catch (Exception ex)
			{
				// Log error
				_logger.LogError(ex, $"Error getting category {id}");

				return StatusCode(500, "Internal server error");
			}
		}

		[HttpPost("CreateCategoryAsync")]
		public async Task<IActionResult> CreateCategoryAsync([FromForm] CategoryDto categoryDto)
		{
			// Validate model
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				// Create category
				var category = new Category
				{
					Name = categoryDto.Name
				};

				// Save
				using (var transaction = _dbContext.Database.BeginTransaction())
				{
					_dbContext.Categories.Add(category);
					await _dbContext.SaveChangesAsync();

					transaction.Commit();
				}

				return Ok(category);

			}
			catch (Exception ex)
			{
				// Log error
				_logger.LogError(ex, "Error creating category");

				return StatusCode(500, "Internal server error");
			}
		}


		[HttpPut("Edit/{id:int}")]
		public async Task<ActionResult> UpdateCategoryAsync(int id, [FromForm] CategoryDto updatedCategoryDto)
		{

			// Validate 
			if (id <= 0 || !ModelState.IsValid)
				return BadRequest();

			try
			{
				// Get old category data
				var oldCategory = await _unitOfWork.Category.FirstOrDefaultAsync(c => c.Id == id);
				if (oldCategory == null)
					return NotFound();

				oldCategory.Name = updatedCategoryDto.Name;


				// Save
				using (var transaction = _dbContext.Database.BeginTransaction())
				{
					_unitOfWork.Category.Update(oldCategory);
                    await _unitOfWork.Save();

                    transaction.Commit();
				}

				// Return response
				return Ok(new
				{
					Name = oldCategory.Name,
				});

			}
			catch (Exception ex)
			{
				// Log error
				_logger.LogError(ex, $"Error updating category {id}");

				return StatusCode(500, "Internal server error");
			}

		}
		[HttpDelete("Delete/{id:int}")]
		public async Task<ActionResult> DeleteCategoryAsync(int id)
		{
			try
			{
				if (id <= 0)
					return BadRequest("Invalid id");

				// Get category
				var category = await _unitOfWork.Category.FirstOrDefaultAsync(c => c.Id == id);
				if (category == null)
					return NotFound();

				
				// Delete subcategories
				var Courses = await _unitOfWork.Course.GetAllAsync(sc => sc.CategoryId == id);
				foreach (var course in Courses)
				{
					_unitOfWork.Course.Delete(course);
				}

				// Delete category
				_unitOfWork.Category.Delete(category);
				await _unitOfWork.Save();

				return Ok("Category deleted");

			}
			catch (Exception ex)
			{
				// Log error
				_logger.LogError(ex, $"Error deleting category {id}");

				return StatusCode(500, "Internal server error");
			}
		}

	}
}