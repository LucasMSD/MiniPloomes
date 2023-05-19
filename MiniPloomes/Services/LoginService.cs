using FluentResults;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MiniPloomes.Config;
using MiniPloomes.Data.Dto;
using MiniPloomes.Data.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniPloomes.Services
{
    public class LoginService
    {
        private readonly AppSettings _settings;
        private readonly UsuarioService _usuarioService;

        public LoginService(IOptions<AppSettings> settings, UsuarioService usuarioService)
        {
            _settings = settings.Value;
            _usuarioService = usuarioService;
        }

        public async Task<Result<ReadLoginTokenDto>> Login(LoginDto login)
        {
            var result = await _usuarioService.Autenticar(login.Email, login.Senha);

            if (result.IsFailed)
            {
                return Result.Fail(result.Errors);
            }

            string token = GerarToken(result.Value);

            return Result.Ok(new ReadLoginTokenDto { Token = token});
        }

        private string GerarToken(Usuario usuario)
        {
            var key = Encoding.ASCII.GetBytes(_settings.SecretKey);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
