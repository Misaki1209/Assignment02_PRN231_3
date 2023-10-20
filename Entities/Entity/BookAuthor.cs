using System.ComponentModel.DataAnnotations;

namespace Entities.Entity;

public class BookAuthor
{
    [Required]
    public int AuthorId { get; set; }
    [Required]
    public int BookId { get; set; }
    [Required]
    public string AuthorOrder { get; set; }
    [Required]
    public int RoyalityPercentage { get; set; }
    
    public virtual Book Book { get; set; }
    public virtual Author Author { get; set; }
}