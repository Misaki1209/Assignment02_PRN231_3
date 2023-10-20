using Entities.Dtos;
using Entities.Entity;
using Entities.RequestModels;

namespace DataAccess.IRepositories;

public interface IBookRepository
{
    public List<BookDto> GetBooks();
    public BookDto? GetBookById(int id);
    public BookDto AddBook(AddBookRequest request);
    public BookDto UpdateBook(UpdateBookRequest request);
    public void DeleteBook(int id);
    public void AddAuthor(AddBookAuthorRequest request);
    public void UpdateAuthor(UpdateBookAuthorRequest request);
    public void DeleteAuthor(int bookId, int authorId);
}