using Entities.Entity;
using Entities.Migration;
using Entities.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Daos;

public class UserDao
{
    public static List<User> GetUsers()
    {
        try
        {
            using var context = new BookDbContext();
            return context.Users.Include(x => x.Role).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static User? GetUserById(int id)
    {
        try
        {
            using var context = new BookDbContext();
            return context.Users.Include(x => x.Role).FirstOrDefault(x => x.UserId == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static User AddUser(User user)
    {
        try
        {
            using var context = new BookDbContext();
            var duplicateMail = context.Users.Any(x => x.Email.ToLower().Equals(user.Email.ToLower()));
            if (duplicateMail)
                throw new Exception("Duplicate email");
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static User UpdateUser(User user)
    {
        try
        {
            using var context = new BookDbContext();
            var oldUser = context.Users.FirstOrDefault(x => x.UserId == user.UserId);
            if (oldUser == null)
                throw new Exception("Not found");
            oldUser.PubId = user.PubId;
            oldUser.FirstName = user.FirstName;
            oldUser.MiddleName = user.MiddleName;
            oldUser.LastName = user.LastName;
            oldUser.HireDate = user.HireDate;
            oldUser.RoleId = user.RoleId;
            oldUser.Source = user.Source;
            context.Users.Update(oldUser);
            context.SaveChanges();
            return oldUser;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static void DeleteUser(int userId)
    {
        try
        {
            using var context = new BookDbContext();
            var user = context.Users.FirstOrDefault(x => x.UserId == userId);
            if (user == null)
                throw new Exception("Not found");
            context.Users.Remove(user);
            context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static User? CheckUserLogin(LoginRequest request)
    {
        try
        {
            using var context = new BookDbContext();
            var user = context.Users.Include(x=>x.Role).FirstOrDefault(x => x.Email == request.Email && x.Password == request.Password);
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}