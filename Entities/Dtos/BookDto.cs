namespace Entities.Dtos;

public class BookDto
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public int PubId { get; set; }
    public decimal Price { get; set; }
    public string Advance { get; set; }
    public decimal Royalty { get; set; }
    public string YtdSales { get; set; }
    public string? Notes { get; set; }
    public DateTime PublishedDate { get; set; }
    public virtual List<BookAuthorDto> BookAuthors { get; set; }
    public virtual PublisherDto Publisher { get; set; }
}