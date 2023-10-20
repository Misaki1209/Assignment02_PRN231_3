using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAccess.IRepositories;
using DataAccess.Repositories;
using Entities.Dtos;
using Entities.RequestModels;
using Entities.ResponseObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.IdentityModel.Tokens;
using WebApis.Common;

namespace WebApis.Controllers;

[Route("[Controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private IUserRepository _userRepository;
    private JwtSetting _jwtSetting;
    private IConfiguration _configuration;

    public UserController(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _jwtSetting = configuration.GetSection("Jwt").Get<JwtSetting>();
        _configuration = configuration;
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var adminAcc = _configuration.GetSection("Admin").Get<LoginRequest>();
        var userLogin = _userRepository.CheckUserLogin(request);
        if (userLogin != null || (string.Equals(request.Email, adminAcc.Email, StringComparison.CurrentCultureIgnoreCase) && request.Password == adminAcc.Password))
        {
            var token = GenerateJwtToken(userLogin);
            return Ok(new LoginResponse() { Token = token });
        }
        return Unauthorized(new { message = "Invalid credentials" });
    }
    private string GenerateJwtToken(UserDto? userLogin)
    {
        var adminAcc = _configuration.GetSection("Admin").Get<LoginRequest>();
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSetting.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("email", userLogin == null?adminAcc.Email:userLogin.Email) }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSetting.DurationInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = "trinhdinhkhai",
            Issuer = "trinhdinhkhai"
        };
        if(userLogin == null)
            tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
        else 
            tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, userLogin.Role.RoleDesc)); 
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    [EnableQuery]
    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<UserDto>> GetUsers() => _userRepository.GetUsers().ToList();
    
    [HttpGet("Get/{id:int}")]
    public ActionResult<UserDto> GetUserById(int id) => _userRepository.GetUserById(id);

    [Authorize(Roles = "Admin")]
    [HttpPost("Add")]
    public ActionResult<UserDto> AddUser(AddUserRequest request) => _userRepository.AddUser(request);

    [Authorize(Roles = "Admin")]
    [HttpPut("Update")]
    public ActionResult<UserDto> UpdateUser(UserDto request) => _userRepository.UpdateUser(request);
}