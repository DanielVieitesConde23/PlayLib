namespace PlayLib.Data.DTOs;

public class CreateTabletopGameDTO {
    public string Name { get; set; }
    public string Description { get; set; }
    public string Creator { get; set; }
    public string Image_Route { get; set; }
    public DateTime Release_Date { get; set; }

    public int MinPlayerNumber { get; set; }
    public int MaxPlayerNumber { get; set; }
    public int AverageDuration { get; set; }

    public List<Guid> Tags { get; set; }
    public List<Guid> Languages { get; set; }
}