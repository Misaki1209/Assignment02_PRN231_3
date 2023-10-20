using Entities.Entity;
using Entities.Migration;

namespace DataAccess.Daos;

public class PublisherDao
{
    public static List<Publisher> GetPublishers()
    {
        try
        {
            using var context = new BookDbContext();
            return context.Publishers.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static Publisher? GetPublisherById(int id)
    {
        try
        {
            using var context = new BookDbContext();
            return context.Publishers.FirstOrDefault(x => x.PubId == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static Publisher AddPublisher(Publisher publisher)
    {
        try
        {
            using var context = new BookDbContext();
            context.Publishers.Add(publisher);
            context.SaveChanges();
            return publisher;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static Publisher UpdatePublisher(Publisher publisher)
    {
        try
        {
            using var context = new BookDbContext();
            var oldPub = context.Publishers.FirstOrDefault(x => x.PubId == publisher.PubId);
            if (oldPub == null)
                throw new Exception("Not found");
            oldPub.PublisherName = publisher.PublisherName;
            oldPub.City = publisher.City;
            oldPub.State = publisher.State;
            oldPub.Country = publisher.Country;
            context.Publishers.Update(oldPub);
            context.SaveChanges();
            return oldPub;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static void DeletePublisher(int id)
    {
        try
        {
            using var context = new BookDbContext();
            var oldPub = context.Publishers.FirstOrDefault(x => x.PubId == id);
            if (oldPub == null)
                throw new Exception("Not found");
            context.Publishers.Remove(oldPub);
            context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}