using Entities.Entity;
using Entities.Migration;

namespace DataAccess.Daos;

public class AuthorDao
{
    public static List<Author> GetAuthors()
    {
        try
        {
            using var context = new BookDbContext();
            return context.Authors.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static Author? GetAuthorById(int id)
    {
        try
        {
            using var context = new BookDbContext();
            return context.Authors.FirstOrDefault(x => x.AuthorId == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static Author AddAuthor(Author request)
    {
        try
        {
            using var context = new BookDbContext();
            context.Authors.Add(request);
            context.SaveChanges();
            return request;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static Author UpdateAuthor(Author author)
    {
        try
        {
            using var context = new BookDbContext();
            var oldAuthor = context.Authors.FirstOrDefault(x => x.AuthorId == author.AuthorId);
            if (oldAuthor == null)
                throw new Exception("Not found");
            oldAuthor.FirstName = author.FirstName;
            oldAuthor.LastName = author.LastName;
            oldAuthor.Address = author.Address;
            oldAuthor.City = author.City;
            oldAuthor.State = author.State;
            oldAuthor.Zip = author.Zip;
            oldAuthor.Phone = author.Phone;
            oldAuthor.EmailAddress = author.EmailAddress;
            context.Authors.Update(oldAuthor);
            context.SaveChanges();
            return oldAuthor;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static void DeleteAuthor(int id)
    {
        try
        {
            using var context = new BookDbContext();
            var author = context.Authors.FirstOrDefault(x => x.AuthorId == id);
            if (author == null)
                throw new Exception("Not found");
            context.Authors.Remove(author);
            context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}