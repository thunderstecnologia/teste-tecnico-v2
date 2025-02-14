using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Thunders.TechTest.ApiService.Models
{
    public class User: IdentityUser
    {
        [Required]
        public required string Role { get; set; }
    }
}
