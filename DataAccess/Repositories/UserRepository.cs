using AutoMapper;
using DataAccess.Daos;
using DataAccess.IRepositories;
using Entities.Dtos;
using Entities.Entity;
using Entities.RequestModels;

namespace DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private IMapper _mapper;

    public UserRepository(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public List<UserDto> GetUsers()
    {
        return _mapper.Map<List<UserDto>>(UserDao.GetUsers());
    }

    public UserDto? GetUserById(int id)
    {
        return _mapper.Map<UserDto>(UserDao.GetUserById(id));
    }

    public UserDto AddUser(AddUserRequest request)
    {
        var user = _mapper.Map<User>(request);
        return _mapper.Map<UserDto>(UserDao.AddUser(user));
    }

    public UserDto UpdateUser(UserDto request)
    {
        var user = _mapper.Map<User>(request);
        return _mapper.Map<UserDto>(UserDao.UpdateUser(user));
    }

    public void DeleteUser(int id)
    {
        UserDao.DeleteUser(id);
    }

    public UserDto? CheckUserLogin(LoginRequest request)
    {
        return _mapper.Map<UserDto>(UserDao.CheckUserLogin(request));
    }
}