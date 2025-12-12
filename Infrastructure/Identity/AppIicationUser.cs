using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    
    [Required]
    public int Idade { get; set; }
}
