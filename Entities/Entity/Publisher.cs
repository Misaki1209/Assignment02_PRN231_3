using System.ComponentModel.DataAnnotations;

namespace Entities.Entity;

public class Publisher
{
    [Key]
    public int PubId { get; set; }
    [Required]
    public string PublisherName { get; set; }
    
    public string? City { get; set; }
    
    public string? State { get; set; }
    
    public string? Country { get; set; }
    
    public virtual List<Book> Books { get; set; }
    public virtual List<User> Users { get; set; }
}