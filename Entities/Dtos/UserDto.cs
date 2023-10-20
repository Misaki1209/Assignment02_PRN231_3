using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos;

public class UserDto
{
    [Key]
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Source { get; set; }
    public int RoleId { get; set; }
    public int PubId { get; set; }
    public DateTime HireDate { get; set; }
    
    public virtual RoleDto Role { get; set; }
    public virtual PublisherDto Publisher { get; set; }
}