using Microsoft.AspNetCore.Mvc;
using MiniPloomes.Data.Dto;
using MiniPloomes.Data.Model;
using MiniPloomes.Services;

namespace MiniPloomes.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            // pegar o id do usuario pela request

            return Ok(await _clienteService.GetClientesAsync(0));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // pegar o id do usuario pela request

            var result = await _clienteService.GetClientesByIdAsync(0, 0);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateClienteDto createCliente)
        {
            // pegar o id do usuario pela request
            int usuarioId = 0;

            Cliente result = await _clienteService.PostClienteAsync(createCliente, usuarioId);


            return CreatedAtAction(nameof(Get), new { result.Id }, result );
        }
    }
}
