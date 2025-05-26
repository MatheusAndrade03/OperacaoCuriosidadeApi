using OperaçãoCuriosidadeApi.Dtos;
using OperaçãoCuriosidadeApi.Models;

namespace OperaçãoCuriosidadeApi.Services
{
    public interface ITokenService
    {
        string? GenerateToken(LoginDto loginDto);
    }
}
