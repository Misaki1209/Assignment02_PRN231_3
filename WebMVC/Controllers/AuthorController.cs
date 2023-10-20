using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Entities.Dtos;
using Entities.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers;

public class AuthorController : Controller
{
    private readonly HttpClient client = null;

    private string apiUrl = "";

    public AuthorController()
    {
        client = new HttpClient();
        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        client.DefaultRequestHeaders.Accept.Add(contentType);
        apiUrl = "https://localhost:7125/Author";
    }
    
    
    public async Task<IActionResult> Index()
    {
        var getAllUrl = apiUrl + "/GetAll";
        var response = await client.GetAsync(getAllUrl);
        var strData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        var listProduct = JsonSerializer.Deserialize<List<AuthorDto>>(strData, options);
        return View(listProduct);
    }

    [HttpPost]
    public async Task<IActionResult> Index(int? authorId, string? firstName, string? lastName, string? city)
    {
        if (authorId == null && string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(city))
            return RedirectToAction("Index");
        var filterUrl = "https://localhost:7125/Author/GetAll?$filter=";
        var firstFilter = true;
        if (authorId != null)
        {
            filterUrl += "AuthorId eq " + authorId;
            firstFilter = false;
            ViewBag.AuthorId = authorId;
        }

        if (firstName != null)
        {
            if (!firstFilter) filterUrl += " and ";
            else firstFilter = false;
            filterUrl += "contains(firstname, '" + firstName + "')";
            ViewBag.FirstName = firstName;
        }
        
        if (lastName != null)
        {
            if (!firstFilter) filterUrl += " and ";
            else firstFilter = false;
            filterUrl += "contains(lastname, '" + lastName + "')";
            ViewBag.LastName = lastName;
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
        var listProduct = JsonSerializer.Deserialize<List<AuthorDto>>(strData, options);
        return View(listProduct);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var addProductRequest = new AddAuthorRequest();
        return View(addProductRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddAuthorRequest request)
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
            var authorDto = JsonSerializer.Deserialize<AuthorDto>(strData, options);
            TempData["success"] = "Author with id = " + authorDto.AuthorId + " is Created done!";
            
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int? id)
    {
        var productDto = new AuthorDto();
        var getAllUrl = apiUrl + "/Get/" + id;
        var response = await client.GetAsync(getAllUrl);
        var strData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        productDto = JsonSerializer.Deserialize<AuthorDto>(strData, options);
        return View(productDto);
    }

    [HttpPost]
    public async Task<IActionResult> Update(AuthorDto request)
    {
        if (ModelState.IsValid)
        {
            var getAllUrl = apiUrl + "/Update";
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["jwt"]);
            Console.WriteLine(JsonSerializer.Serialize(request));
            var response = await client.PutAsync(getAllUrl, jsonContent);
            var strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            var authorDto = JsonSerializer.Deserialize<AuthorDto>(strData, options);
            TempData["success"] = "Author with id = " + authorDto.AuthorId + " is Updated done!";
        }
        return RedirectToAction("Index");
    }
}