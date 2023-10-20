using DataAccess.IRepositories;
using Entities.Dtos;
using Entities.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace WebApis.Controllers;

[Route("[controller]")]
[ApiController]
public class PublisherController : ControllerBase
{
    private IPublisherRepository _publisherRepository;

    public PublisherController(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    [EnableQuery]
    [HttpGet("Get")]
    public IActionResult GetPublishers()
    {
        return Ok(_publisherRepository.GetPublishers());
    }

    [HttpGet("Get/{id:int}")]
    public IActionResult GetPublisherById(int id)
    {
        return Ok(_publisherRepository.GetPublisherById(id));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("Add")]
    public IActionResult AddPublisher(AddPublisherRequest request)
    {
        return Ok(_publisherRepository.AddPublisher(request));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("Update")]
    public IActionResult UpdatePublisher(PublisherDto publisherDto)
    {
        return Ok(_publisherRepository.UpdatePublisher(publisherDto));
    } 

    [Authorize(Roles = "Admin")]
    [HttpDelete("Delete/{id:int}")]
    public IActionResult DeletePublisher(int id)
    {
        _publisherRepository.DeletePublisher(id);
        return Ok();
    }
}