namespace Entities.RequestModels;

public class AddUserRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Source { get; set; }
    public int RoleId { get; set; }
    public int PubId { get; set; }
    public DateTime HireDate { get; set; }
}