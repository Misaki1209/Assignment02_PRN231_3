using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entity;

public class User
{
    [Key]
    public int UserId { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string MiddleName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Source { get; set; }
    [Required]
    public int RoleId { get; set; }
    [Required]
    public int PubId { get; set; }
    [Required]
    public DateTime HireDate { get; set; }
    
    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; }
    [ForeignKey("PubId")]
    public virtual Publisher Publisher { get; set; }
}