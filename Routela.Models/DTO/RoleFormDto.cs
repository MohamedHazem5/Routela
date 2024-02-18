using System.ComponentModel.DataAnnotations;

namespace Routela.Models.DTO
{
    public class RoleFormDto
    {
        [Required, StringLength(256)]
        public string Name { get; set; }

    }
}
