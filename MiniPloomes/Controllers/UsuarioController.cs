using Microsoft.AspNetCore.Mvc;
using MiniPloomes.Data.Dto;
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
            var result = await _usuarioService.GetById(id);

            if (result.IsFailed)
            {
                return NotFound();
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUsuarioDto createUsuarioDto)
        {
            var result = await _usuarioService.Post(createUsuarioDto);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.Select(x => x.Message));
            }

            return CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value);
        }
    }
}
