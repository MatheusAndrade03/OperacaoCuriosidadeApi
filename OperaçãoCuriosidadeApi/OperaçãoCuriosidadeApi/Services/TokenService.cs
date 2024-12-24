using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OperaçãoCuriosidadeApi.Command;
using OperaçãoCuriosidadeApi.Dtos;
using OperaçãoCuriosidadeApi.Models;
using OperaçãoCuriosidadeApi.Query;

namespace OperaçãoCuriosidadeApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UsuariosCommand _usuariosCommand;
        private readonly UsuariosQuery _usuariosQuery;
        private readonly ISecurityService _securityService;
        public TokenService(IConfiguration configuration, UsuariosCommand usuariosCommand, UsuariosQuery usuariosQuery,ISecurityService securityService)
        {
            _configuration = configuration;
            _usuariosCommand = usuariosCommand;
            _usuariosQuery = usuariosQuery;
            _securityService = securityService;
        }

        public  string? GenerateToken(LoginDto login)
        {
            

            var usuarioDataBase = _usuariosQuery.GetByEmail(login.Email);
            if (usuarioDataBase is null) return String.Empty;

            bool senhaValida = _securityService.VerifyPassword(login.Senha, usuarioDataBase.Senha);
            if (!senhaValida)
            {
                return string.Empty;
            }

            var chaveSecreta = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"] ?? string.Empty));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var signingCredentials = new SigningCredentials(chaveSecreta, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.Now.AddHours(1),
                claims: new[]
                {
                    new Claim(ClaimTypes.Name, usuarioDataBase.NomeUsuario),
                    new Claim(ClaimTypes.Role, usuarioDataBase.Admin?"Admin":"Colaborador"),
                    new Claim(ClaimTypes.Email, usuarioDataBase.Email),
                    new Claim(ClaimTypes.NameIdentifier, usuarioDataBase.Id.ToString()),
                },
                signingCredentials: signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenHandler;
        }
    }
}
