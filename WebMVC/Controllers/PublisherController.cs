using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Entities.Dtos;
using Entities.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers;

public class PublisherController : Controller
{
    private readonly HttpClient client = null;

    private string apiUrl = "";

    public PublisherController()
    {
        client = new HttpClient();
        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        client.DefaultRequestHeaders.Accept.Add(contentType);
        apiUrl = "https://localhost:7125/Publisher";
    }
    
    
    public async Task<IActionResult> Index()
    {
        var getAllUrl = apiUrl + "/Get";
        var response = await client.GetAsync(getAllUrl);
        var strData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        var listPublisher = JsonSerializer.Deserialize<List<PublisherDto>>(strData, options);
        return View(listPublisher);
    }
    
    [HttpPost]
    public async Task<IActionResult> Index(int? pubId, string? publisherName, string? city)
    {
        if (pubId == null && string.IsNullOrEmpty(publisherName) && string.IsNullOrEmpty(city))
            return RedirectToAction("Index");
        var filterUrl = "https://localhost:7125/Publisher/Get?$filter=";
        var firstFilter = true;
        if (pubId != null)
        {
            filterUrl += "PubId eq " + pubId;
            firstFilter = false;
            ViewBag.PubId = pubId;
        }

        if (publisherName != null)
        {
            if (!firstFilter) filterUrl += " and ";
            else firstFilter = false;
            filterUrl += "contains(publishername, '" + publisherName + "')";
            ViewBag.PublisherName = publisherName;
        }
        
        if (city != null)
        {
            if (!firstFilter) filterUrl += " and ";
            else firstFilter = false;
            filterUrl += "contains(city, '" + city + "')";
            ViewBag.City = city;
        }
        
        var response = await client.GetAsync(filterUrl);
        var strData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        var listProduct = JsonSerializer.Deserialize<List<PublisherDto>>(strData, options);
        return View(listProduct);
    }

    [HttpGet]
    public IActionResult Create(int? id)
    {
        var addPublisherRequest = new AddPublisherRequest();
        return View(addPublisherRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddPublisherRequest request)
    {
        if (ModelState.IsValid)
        {
            var getAllUrl = apiUrl + "/Add";
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["jwt"]);
            var response = await client.PostAsync(getAllUrl, jsonContent);
            var strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            var publisherDto = JsonSerializer.Deserialize<PublisherDto>(strData, options);
            TempData["success"] = "Publisher with id = " + publisherDto.PubId + " is Created done!";
            
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int? id)
    {
        var publisherDto = new PublisherDto();
        var getAllUrl = apiUrl + "/Get/" + id;
        var response = await client.GetAsync(getAllUrl);
        var strData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        publisherDto = JsonSerializer.Deserialize<PublisherDto>(strData, options);
        return View(publisherDto);
    }

    [HttpPost]
    public async Task<IActionResult> Update(PublisherDto request)
    {
        if (ModelState.IsValid)
        {
            var getAllUrl = apiUrl + "/Update";
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["jwt"]);
            Console.WriteLine(JsonSerializer.Serialize(request));
            var response = await client.PostAsync(getAllUrl, jsonContent);
            var strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            var publisherDto = JsonSerializer.Deserialize<PublisherDto>(strData, options);
            TempData["success"] = "Publisher with id = " + publisherDto.PubId + " is Updated done!";
        }
        return RedirectToAction("Index");
    }
}