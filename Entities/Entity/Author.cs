using System.ComponentModel.DataAnnotations;

namespace Entities.Entity;

public class Author
{
    [Key]
    public int AuthorId { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    public string EmailAddress { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string State { get; set; }
    [Required]
    public string Zip { get; set; }
    public virtual List<BookAuthor> BookAuthors { get; set; }
}