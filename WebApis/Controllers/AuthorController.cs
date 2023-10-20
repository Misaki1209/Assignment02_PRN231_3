using DataAccess.IRepositories;
using Entities.Dtos;
using Entities.Entity;
using Entities.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace WebApis.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private IAuthorRepository _authorRepository;

    public AuthorController(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    [EnableQuery]
    [HttpGet("GetAll")]
    public IActionResult GetAuthors()
    {
        return Ok(_authorRepository.GetAuthors());
    }

    [HttpGet("Get/{id:int}")]
    public IActionResult GetAuthorById(int id)
    {
        return Ok(_authorRepository.GetAuthorById(id));
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("Add")]
    public ActionResult<AuthorDto> AddAuthor(AddAuthorRequest request) => _authorRepository.AddAuthor(request);

    [Authorize(Roles = "Admin")]
    [HttpPut("Update")]
    public ActionResult<AuthorDto> UpdateAuthor(AuthorDto request) => _authorRepository.UpdateAuthor(request);
}