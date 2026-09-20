using System.ComponentModel.DataAnnotations;

namespace UrlShortner.DTOs;

public class RegisterUser
{
    [Required(ErrorMessage = "Username is required")]
    [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
    public string Username { get; set; } = string.Empty;
    
    [Required(ErrorMessage ="Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set;} = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", 
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
    [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
    public string Password { get; set; } = string.Empty;
}