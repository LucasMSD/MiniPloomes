using Microsoft.AspNetCore.Mvc;
using MiniPloomes.Data.Dto;
using MiniPloomes.Data.Model;
using MiniPloomes.Services;

namespace MiniPloomes.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _usuarioService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _usuarioService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUsuarioDto createUsuarioDto)
        {
            Usuario result = await _usuarioService.Post(createUsuarioDto);

            return CreatedAtAction(nameof(Get), new { result.Id }, result);
        }
    }
}
