using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.CodeAnalysis;

namespace Routela.Models
{
    public class User: IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public ICollection<AppUserRole> UserRoles { get; set; }
        public int ImageId { get; set; }
        [AllowNull]
        public Image Image { get; set; }

    }
}
