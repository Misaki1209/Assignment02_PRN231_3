using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Entities.Dtos;
using Entities.Entity;
using Entities.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers;

public class BookController : Controller
{
    private readonly HttpClient client = null;

    private string apiUrl = "";

    public BookController()
    {
        client = new HttpClient();
        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        client.DefaultRequestHeaders.Accept.Add(contentType);
        apiUrl = "https://localhost:7125/Book";
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
        var listBook = JsonSerializer.Deserialize<List<BookDto>>(strData, options);
        return View(listBook);
    }

    [HttpPost]
    public async Task<IActionResult> Index(int? bookId, string? title, string? type)
    {
        if (bookId == null && string.IsNullOrEmpty(title) && string.IsNullOrEmpty(type))
            return RedirectToAction("Index");
        var filterUrl = "https://localhost:7125/Book/Get?$filter=";
        var firstFilter = true;
        if (bookId != null)
        {
            filterUrl += "BookId eq " + bookId;
            firstFilter = false;
            ViewBag.BookId = bookId;
        }

        if (title != null)
        {
            if (!firstFilter) filterUrl += " and ";
            else firstFilter = false;
            filterUrl += "contains(title, '" + title + "')";
            ViewBag.BookTitle = title;
        }

        if (type != null)
        {
            if (!firstFilter) filterUrl += " and ";
            else firstFilter = false;
            filterUrl += "contains(type, '" + type + "')";
            ViewBag.Type = type;
        }

        var response = await client.GetAsync(filterUrl);
        var strData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        var listProduct = JsonSerializer.Deserialize<List<BookDto>>(strData, options);
        return View(listProduct);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        const string getAllUrl = "https://localhost:7125/Publisher/Get";
        var response = await client.GetAsync(getAllUrl);
        var strData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        var listPublisher = JsonSerializer.Deserialize<List<PublisherDto>>(strData, options);
        ViewBag.Publishers = listPublisher;
        var request = new AddBookRequest();
        return View(request);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddBookRequest request)
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
            var bookDto = JsonSerializer.Deserialize<BookDto>(strData, options);
            TempData["success"] = "Book with id = " + bookDto.BookId + " is Created done!";
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int? id)
    {
        const string getPublisherUrl = "https://localhost:7125/Publisher/Get";
        var response1 = await client.GetAsync(getPublisherUrl);
        var strData1 = await response1.Content.ReadAsStringAsync();
        var options1 = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        var listPublisher = JsonSerializer.Deserialize<List<PublisherDto>>(strData1, options1);
        ViewBag.Publishers = listPublisher;

        var bookDto = new BookDto();
        var getAllUrl = apiUrl + "/Get/" + id;
        var response = await client.GetAsync(getAllUrl);
        var strData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        bookDto = JsonSerializer.Deserialize<BookDto>(strData, options);
        return View(bookDto);
    }

    [HttpPost]
    public async Task<IActionResult> Update(BookDto request)
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
        var bookDto = JsonSerializer.Deserialize<BookDto>(strData, options);
        TempData["success"] = "Book with id = " + bookDto.BookId + " is Updated done!";

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Detail(int id)
    {
            const string getAuthorUrl = "https://localhost:7125/Author/GetAll";
        var response1 = await client.GetAsync(getAuthorUrl);
        var strData1 = await response1.Content.ReadAsStringAsync();
        var options1 = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        var listAuthor = JsonSerializer.Deserialize<List<AuthorDto>>(strData1, options1);
        ViewBag.Authors = listAuthor;
        
        var bookDto = new BookDto();
        var getAllUrl = apiUrl + "/Get/" + id;
        var response = await client.GetAsync(getAllUrl);
        var strData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        bookDto = JsonSerializer.Deserialize<BookDto>(strData, options);
        ViewBag.SelectedBookDto = bookDto;
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> UpdateAuthor(int bookId, int authorId)
    {
        const string getAuthorUrl = "https://localhost:7125/Author/GetAll";
        var response1 = await client.GetAsync(getAuthorUrl);
        var strData1 = await response1.Content.ReadAsStringAsync();
        var options1 = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        var listAuthor = JsonSerializer.Deserialize<List<AuthorDto>>(strData1, options1);
        ViewBag.Authors = listAuthor;
        
        var bookDto = new BookDto();
        var getAllUrl = apiUrl + "/Get/" + bookId;
        var response = await client.GetAsync(getAllUrl);
        var strData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        bookDto = JsonSerializer.Deserialize<BookDto>(strData, options);
        var bookAuthor = bookDto.BookAuthors.FirstOrDefault(x => x.BookId == bookId && x.AuthorId == authorId);
        ViewBag.UpdateBookAuthor = bookAuthor;
        ViewBag.SelectedBookDto = bookDto;
        return View("Detail");
    }

    /*[HttpPost]
    public async Task<IActionResult> UpdateAuthor(UpdateBookAuthorRequest request)
    {
        var getAllUrl = apiUrl + "/UpdateBookAuthor";
        var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["jwt"]);
        Console.WriteLine(JsonSerializer.Serialize(request));
        await client.PostAsync(getAllUrl, jsonContent);

        return RedirectToAction("Detail", request.BookId);
    }*/

    [HttpPost]
    public async Task<IActionResult> AddAuthor(AddBookAuthorRequest request)
    {
        var getAllUrl = apiUrl + "/AddBookAuthor";
        var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["jwt"]);
        await client.PostAsync(getAllUrl, jsonContent);

        return RedirectToAction("Detail", new{id = request.BookId});
    }
    
    [HttpGet]
    public async Task<IActionResult> DeleteAuthor(int bookId, int authorId)
    {
        var getAllUrl = apiUrl + "/DeleteBookAuthor/" + bookId +"/"+authorId;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["jwt"]);
        await client.PostAsync(getAllUrl, null);

        return RedirectToAction("Detail", new{id = bookId});
    }
}