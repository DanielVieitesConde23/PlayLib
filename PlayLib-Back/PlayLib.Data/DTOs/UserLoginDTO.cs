using System.ComponentModel.DataAnnotations;

namespace PlayLib.Data.DTOs;

public class UserLoginDto {
    public string LoginInfo { get; set; }

    [Required]
    public string Password { get; set; }
}
