using FluentResults;
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

        public async Task<List<ReadUsuarioDto>> GetAll()
        {
            var usuarios = await _repository.FindAll();

            return usuarios.Select(x => new ReadUsuarioDto
            {
                Id = x.Id,
                Nome = x.Nome,
                Email = x.Email,
                Created = x.Created
            }).ToList();
        }

        public async Task<Result<ReadUsuarioDto>> GetById(int id)
        {
            var usuario = await _repository.FindById(id);

            if (usuario == null)
            {
                return Result.Fail("Usuario não existe");
            }

            return Result.Ok(new ReadUsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Created = usuario.Created
            });
        }

        public async Task<Result<ReadUsuarioDto>> Post(CreateUsuarioDto createUsuarioDto)
        {
            if (await _repository.ExistsByEmail(createUsuarioDto.Email))
            {
                return Result.Fail("Este email já está sendo usado");
            }

            var usuario = await _repository.Insert(createUsuarioDto);

            return new ReadUsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }

        public async Task<Result<Usuario>> Autenticar(string email, string senha)
        {
            var usuario = await _repository.FindUserByLogin(email, senha);

            if (usuario == null)
            {
                return Result.Fail("Login incorreto");
            }

            return Result.Ok(usuario);
        }
    }
}
