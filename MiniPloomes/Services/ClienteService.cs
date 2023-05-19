using MiniPloomes.Data.Dto;
using MiniPloomes.Data.Model;
using MiniPloomes.Repository;

namespace MiniPloomes.Services
{
    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository;

        public ClienteService (ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }


        public async Task<List<Cliente>> GetClientesAsync(int usuarioid)
        {
            return await _clienteRepository.FindAllByUsuario(usuarioid);
        }

        public async Task<Cliente?> GetClientesByIdAsync(int usuarioId, int clienteId)
        {
            return await _clienteRepository.FindById(usuarioId, clienteId);
        }

        public async Task<Cliente> PostClienteAsync(CreateClienteDto createCliente, int usuarioId)
        {
            return await _clienteRepository.Insert(createCliente, usuarioId);
        }
    }
}
