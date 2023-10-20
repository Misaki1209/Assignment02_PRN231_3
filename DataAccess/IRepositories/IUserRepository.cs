using Entities.Dtos;
using Entities.Entity;
using Entities.RequestModels;

namespace DataAccess.IRepositories;

public interface IUserRepository
{
    public List<UserDto> GetUsers();
    public UserDto? GetUserById(int id);
    public UserDto AddUser(AddUserRequest request);
    public UserDto UpdateUser(UserDto request);
    public void DeleteUser(int id);
    public UserDto? CheckUserLogin(LoginRequest request);
}