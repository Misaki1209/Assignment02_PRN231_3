using Entities.Dtos;
using Entities.RequestModels;

namespace DataAccess.IRepositories;

public interface IAuthorRepository
{
    public List<AuthorDto> GetAuthors();
    public AuthorDto? GetAuthorById(int id);
    public AuthorDto AddAuthor(AddAuthorRequest request);
    public AuthorDto UpdateAuthor(AuthorDto request);
    public void DeleteAuthor(int id);
}