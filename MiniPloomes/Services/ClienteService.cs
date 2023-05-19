using FluentResults;
using MiniPloomes.Data.Dto;
using MiniPloomes.Data.Model;
using MiniPloomes.Repository;

namespace MiniPloomes.Services
{
    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository;
        private readonly UsuarioRepository _usuarioRepository;

        public ClienteService (ClienteRepository clienteRepository, UsuarioRepository usuarioRepository)
        {
            _clienteRepository = clienteRepository;
            _usuarioRepository = usuarioRepository;
        }


        public async Task<List<Cliente>> GetClientesAsync(int usuarioid)
        {
            return await _clienteRepository.FindAllByUsuario(usuarioid);
        }

        public async Task<Result<Cliente>> GetClientesByIdAsync(int usuarioId, int clienteId)
        {
            var cliente = await _clienteRepository.FindById(usuarioId, clienteId);

            if (cliente == null)
            {
                return Result.Fail("Id do cliente não existe");
            }

            return Result.Ok(cliente);
        }

        public async Task<Cliente> PostClienteAsync(CreateClienteDto createCliente, int usuarioId)
        {
            return await _clienteRepository.Insert(createCliente, usuarioId);
        }
    }
}
