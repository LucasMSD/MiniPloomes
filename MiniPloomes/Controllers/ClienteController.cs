using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniPloomes.Data.Dto;
using MiniPloomes.Data.Model;
using MiniPloomes.Services;
using System.Security.Claims;

namespace MiniPloomes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int usuarioId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(await _clienteService.GetClientesAsync(usuarioId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            int usuarioId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _clienteService.GetClientesByIdAsync(usuarioId, id);

            if (result.IsFailed)
            {
                return NotFound();
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateClienteDto createCliente)
        {
            int usuarioId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Cliente result = await _clienteService.PostClienteAsync(createCliente, usuarioId);

            return CreatedAtAction(nameof(Get), new { result.Id }, result );
        }
    }
}
