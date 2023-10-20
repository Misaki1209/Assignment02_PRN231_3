using System.ComponentModel.DataAnnotations;

namespace Entities.Entity;

public class Book
{
    [Key]
    public int BookId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    public int PubId { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string Advance { get; set; }
    [Required]
    public decimal Royalty { get; set; }
    [Required]
    public string YtdSales { get; set; }
    public string? Notes { get; set; }
    [Required]
    public DateTime PublishedDate { get; set; }
    public virtual List<BookAuthor> BookAuthors { get; set; }
    public virtual Publisher Publisher { get; set; }
}