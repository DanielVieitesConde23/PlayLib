namespace PlayLib.Data.DTOs;

public class UpdatePasswordDto {
    public Guid UserId { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string RepeatNewPassword { get; set; }
}
