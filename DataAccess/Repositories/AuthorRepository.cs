using AutoMapper;
using DataAccess.Daos;
using DataAccess.IRepositories;
using Entities.Dtos;
using Entities.Entity;
using Entities.Migration;
using Entities.RequestModels;

namespace DataAccess.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private IMapper _mapper;

    public AuthorRepository(IMapper mapper)
    {
        _mapper = mapper;
    }
    public List<AuthorDto> GetAuthors()
    {
        try
        {
            var context = new BookDbContext();
            return _mapper.Map <List<AuthorDto>>(AuthorDao.GetAuthors());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public AuthorDto? GetAuthorById(int id)
    {
        try
        {
            using var context = new BookDbContext();
            return _mapper.Map<AuthorDto>(AuthorDao.GetAuthorById(id));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public AuthorDto AddAuthor(AddAuthorRequest request)
    {
        var author = _mapper.Map<Author>(request);
        return _mapper.Map<AuthorDto>(AuthorDao.AddAuthor(author));
    }

    public AuthorDto UpdateAuthor(AuthorDto request)
    {
        var author = _mapper.Map<Author>(request);
        return _mapper.Map<AuthorDto>(AuthorDao.UpdateAuthor(author));
    }

    public void DeleteAuthor(int id)
    {
        AuthorDao.DeleteAuthor(id);
    }
}