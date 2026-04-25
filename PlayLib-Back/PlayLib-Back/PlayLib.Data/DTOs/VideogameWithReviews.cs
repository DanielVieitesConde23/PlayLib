namespace PlayLib.Data.DTOs;

public class VideogameWithReviews {
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Developer { get; set; } = null!;
    public string? ImageRoute { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string Format { get; set; } = null!;
    public string State { get; set; } = null!;
    public bool IsFavourite { get; set; } 
    public bool IsInLibrary { get; set; }
    public ICollection<ReviewDTO> Reviews { get; set; }
    public ICollection<TagDto> Tags { get; set; }
    public ICollection<LanguageDTO> Languages { get; set; }
}
