using MiniPloomes.Data.Dto;
using MiniPloomes.Data.Model;
using MiniPloomes.Repository;

namespace MiniPloomes.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repository;

        public UsuarioService(UsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Usuario>> GetAll()
        {
            return await _repository.FindAll();
        }

        public async Task<object?> GetById(int id)
        {
            return await _repository.FindById(id);
        }

        public async Task<Usuario> Post(CreateUsuarioDto createUsuarioDto)
        {
            return await _repository.Insert(createUsuarioDto);
        }
    }
}
