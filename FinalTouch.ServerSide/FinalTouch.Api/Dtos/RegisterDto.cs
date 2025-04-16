using System;
using System.ComponentModel.DataAnnotations;

namespace FinalTouch.Api.Dtos;

public class RegisterDto
{
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Username Password is required")]
    public string Password { get; set; } = string.Empty;
}
