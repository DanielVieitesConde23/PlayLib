using System.ComponentModel.DataAnnotations.Schema;

namespace PlayLib.Data.Entities;

public class Request {
    public Guid Id { get; set; }

    [Column("User_Id")]
    public Guid UserId { get; set; }

    [Column("Game_Name")]
    public string GameName { get; set; } = null!;
    public bool IsTabletop { get; set; }
    public string? Description { get; set; }
    public bool? Approved { get; set; }

    public User User { get; set; } = null!;
}