using DataAccess.IRepositories;
using Entities.Dtos;
using Entities.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace WebApis.Controllers;

[Route("[controller]")]
[ApiController]
public class BookController : Controller
{
    private IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    [EnableQuery]
    [HttpGet("Get")]
    public IActionResult GetBooks()
    {
        return Ok(_bookRepository.GetBooks());
    }

    [HttpGet("Get/{id:int}")]
    public IActionResult GetBookById(int id)
    {
        return Ok(_bookRepository.GetBookById(id));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("Add")]
    public IActionResult AddBook(AddBookRequest request)
    {
        return Ok(_bookRepository.AddBook(request));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("Update")]
    public IActionResult UpdateBook(UpdateBookRequest request)
    {
        return Ok(_bookRepository.UpdateBook(request));
    } 

    [Authorize(Roles = "Admin")]
    [HttpDelete("Delete/{id:int}")]
    public IActionResult DeleteBook(int id)
    {
        _bookRepository.DeleteBook(id);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("AddBookAuthor")]
    public IActionResult AddBookAuthor(AddBookAuthorRequest request)
    {
        _bookRepository.AddAuthor(request);
        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("UpdateBookAuthor")]
    public IActionResult UpdateBookAuthor(UpdateBookAuthorRequest request)
    {
        _bookRepository.UpdateAuthor(request);
        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("DeleteBookAuthor/{bookId:int}/{authorId:int}")]
    public IActionResult DeleteBookAuthor(int bookId, int authorId)
    {
        _bookRepository.DeleteAuthor(bookId, authorId);
        return Ok();
    }
}