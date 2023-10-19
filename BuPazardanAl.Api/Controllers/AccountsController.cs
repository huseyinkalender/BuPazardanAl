using BuPazardanAl.Business.Abstract;
using BuPazardanAl.Entities.Concrete.Dtos;
using BuPazardanAl.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuPazardanAl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AccountsController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            string token = await _authService.CreateToken(loginDto);
            return token is not null ? Ok(token) : NotFound("Başarısız giriş...");
        }
        [HttpPost]
        public async Task<IActionResult> SellerRegister(RegisterDto registerDto)
        {
            var result = await _authService.Register(registerDto);
            return result.Succeeded? Ok(result) : NotFound("Üye işlemi başarısız...");
        }
        [HttpPost]
        [Route("/CustomerRegister")]
        public async Task<IActionResult> CustomerRegister(CustomerRegisterDto registerDto)
        {
            var result = await _authService.CustomerRegister(registerDto);
            return result.Succeeded ? Ok(result) : NotFound("Üye işlemi başarısız...");
        }
    }
}
