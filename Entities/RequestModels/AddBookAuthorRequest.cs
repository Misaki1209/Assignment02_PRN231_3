namespace Entities.RequestModels;

public class AddBookAuthorRequest
{
    public int AuthorId { get; set; }
    public int BookId { get; set; }
    public string AuthorOrder { get; set; }
    public int RoyalityPercentage { get; set; }
}