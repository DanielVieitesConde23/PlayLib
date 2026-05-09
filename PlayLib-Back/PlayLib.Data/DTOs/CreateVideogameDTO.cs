namespace PlayLib.Data.DTOs;

public class CreateVideogameDTO {
    public string Name { get; set; }
    public string Description { get; set; }
    public string Developer { get; set; }
    public string Image_Route { get; set; }
    public DateTime Release_Date { get; set; }
    public List<Guid> Tags { get; set; }
    public List<Guid> Languages { get; set; }
}
