using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniPloomes.Data.Dto;
using MiniPloomes.Services;

namespace MiniPloomes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginDto login)
        {
            var result = await _loginService.Login(login);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.Select(x => x.Message));
            }

            return Ok(result.Value);
        }
    }
}
