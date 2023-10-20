using System.ComponentModel.DataAnnotations;

namespace Entities.Entity;

public class Role
{
    [Key]
    public int RoleId { get; set; }
    [Required]
    public string RoleDesc { get; set; }
}