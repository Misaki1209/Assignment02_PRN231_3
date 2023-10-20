using AutoMapper;
using DataAccess.Daos;
using DataAccess.IRepositories;
using Entities.Dtos;
using Entities.Entity;
using Entities.RequestModels;

namespace DataAccess.Repositories;

public class BookRepository : IBookRepository
{
    private IMapper _mapper;

    public BookRepository(IMapper mapper)
    {
        _mapper = mapper;
    }
    public List<BookDto> GetBooks()
    {
        return _mapper.Map<List<BookDto>>(BookDao.GetBooks());
    }

    public BookDto? GetBookById(int id)
    {
        return _mapper.Map<BookDto>(BookDao.GetBookById(id));
    }

    public BookDto AddBook(AddBookRequest request)
    {
        var book = _mapper.Map<Book>(request);
        return _mapper.Map<BookDto>(BookDao.AddBook(book));
    }

    public BookDto UpdateBook(UpdateBookRequest request)
    {
        var book = _mapper.Map<Book>(request);
        return _mapper.Map<BookDto>(BookDao.UpdateBook(book));
    }

    public void DeleteBook(int id)
    {
       BookDao.DeleteBook(id);
    }

    public void AddAuthor(AddBookAuthorRequest request)
    {
        var bookAuthor = _mapper.Map<BookAuthor>(request);
        BookDao.AddAuthor(bookAuthor);
    }

    public void UpdateAuthor(UpdateBookAuthorRequest request)
    {
        var bookAuthor = _mapper.Map<BookAuthor>(request);
        BookDao.UpdateAuthor(bookAuthor);
    }

    public void DeleteAuthor(int bookId, int authorId)
    {
        BookDao.DeleteAuthor(bookId, authorId);
    }
}