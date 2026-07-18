using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.DTOs.Auth;

public class RegisterDTO
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
    
    [Required]
    public int NationalID { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, MinLength(8)]
    public string Password { get; set; }
}