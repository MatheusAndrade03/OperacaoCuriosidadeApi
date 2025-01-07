using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperaçãoCuriosidadeApi.Dtos;
using OperaçãoCuriosidadeApi.Models;
using OperaçãoCuriosidadeApi.Services;

namespace OperaçãoCuriosidadeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly ITokenService _tokenService;
        public AuthenticationController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login",Name ="login")]
        public IActionResult Login( LoginDto? loginDto)
        {
            var token = _tokenService.GenerateToken(loginDto);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("email ou senha invalidos");
            }
            var Token = new {token};
            return Ok(Token);
        }

    }
}
