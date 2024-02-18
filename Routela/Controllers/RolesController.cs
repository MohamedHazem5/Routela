using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Routela.Models;
using Routela.Models.DTO;


namespace Routela.Controllers
{

    public class RolesController : BaseApiController
    {

        private readonly RoleManager<AppRole> _roleManager;

        public RolesController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(roles);
        }

        [HttpPost]
        [Route("Seed")]
        public async Task<IActionResult> SeedRoles()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole
                {
                    Name = "User",
                });
                return Ok();
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleFormDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(await _roleManager.Roles.ToListAsync());

            if (await _roleManager.RoleExistsAsync(model.Name))
            {
                ModelState.AddModelError("Name", "Role is exists!");
                return BadRequest(await _roleManager.Roles.ToListAsync());
            }

            await _roleManager.CreateAsync(new AppRole 
            {
               Name = model.Name.Trim() 
            }
            );

            return Ok();
        }
    }
}