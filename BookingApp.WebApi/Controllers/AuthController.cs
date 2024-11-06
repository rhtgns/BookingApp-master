using BookingApp.Business.Operations.User;
using BookingApp.Business.Operations.User.Dtos;
using BookingApp.WebApi.Jwt;
using BookingApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(Models.RegisterRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //TODO: ileride ActionFİlter olarak kodlanıcak
            var addUserDto = new AddUserDto
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                BirthDate = request.BirthDate,
            };
            var result = await _userService.AddUser(addUserDto);
            if(result.IsSucceed)
                return Ok();
            else
                return BadRequest(result.Message);
        }

        [HttpPost("Login")]
        public IActionResult Login(Models.LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _userService.LoginUser(new LoginUserDto { Email = request.Email, Password = request.Password });

            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }

            var user = result.Data;

            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var token = JwtHelper.GenerateJwtToken(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]!)
            });

            return Ok(new LoginResponse
            {
                Message = "Giriş başarıyla Gerçekleşti",
                Token = token
            });

        }

        [HttpGet("me")]
        [Authorize] //token yok ise ,cevap yok
        public IActionResult GetMyUser()
        {
            return Ok();
        }


    }
}
