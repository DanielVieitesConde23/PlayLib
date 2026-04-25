using System.ComponentModel.DataAnnotations;

namespace PlayLib.Data.DTOs;

public class UserRegisterDto {
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required, Compare(nameof(Password))]
    public string RepeatPassword { get; set; }
}
