using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Entities.RequestModels;
using Entities.ResponseObjects;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers;

public class LoginController : Controller
{
    private readonly HttpClient client = null;

    private string apiUrl = "";

    public LoginController()
    {
        client = new HttpClient();
        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        client.DefaultRequestHeaders.Accept.Add(contentType);
        apiUrl = "https://localhost:7125/User/Login";
    }
    // GET
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string email, string password)
    {
        var jwt = await LoginAsync(email, password);
        if (!string.IsNullOrEmpty(jwt))
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddHours(1)
            };
            Response.Cookies.Append("jwt", jwt, cookieOptions);
            return RedirectToAction("Index", "Home");
        }

        else
        {
            ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
            return View("Index");
        }
    }
    
    public async Task<string> LoginAsync(string email, string password)
    {
        var loginRequest = new LoginRequest()
        {
            Email = email,
            Password = password
        };
        var jsonContent = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");
        var response = await client.PostAsync(apiUrl,jsonContent);
        var strData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        var jwt = JsonSerializer.Deserialize<LoginResponse>(strData, options);
        return jwt.Token;
    }
}