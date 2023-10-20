namespace Entities.Dtos;

public class BookAuthorDto
{
    public int AuthorId { get; set; }
    public int BookId { get; set; }
    public string AuthorOrder { get; set; }
    public int RoyalityPercentage { get; set; }
    public virtual AuthorDto Author { get; set; }
}