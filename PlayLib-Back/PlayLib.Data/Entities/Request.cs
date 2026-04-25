using PlayLib.Data.Entities;

public class Request {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string GameName { get; set; } = null!;
    public bool IsTabletop { get; set; }
    public string? Description { get; set; }
    public bool? Approved { get; set; }

    public User User { get; set; } = null!;
}