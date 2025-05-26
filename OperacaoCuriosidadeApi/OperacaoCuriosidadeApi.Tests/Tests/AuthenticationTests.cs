using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using OperacaoCuriosidadeApi;
using OperaçãoCuriosidadeApi.Dtos;
using OperacaoCuriosidadeApi.Tests.Helpers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace OperacaoCuriosidadeApi.Tests
{
    public class AuthenticationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AuthenticationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "Deve autenticar com credenciais válidas e retornar token")]
        public async Task Login_ComCredenciaisValidas_DeveRetornarToken()
        {
            // Arrange
            var login = new LoginDto
            {
                Email = "admin@teste.com",
                Senha = "123456" // Essa senha foi hasheada na CustomWebApplicationFactory
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/authentication/login", login);

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            // Verifica se contém o campo "token"
            Assert.Contains("token", content);
        }

        [Fact(DisplayName = "Não deve autenticar com credenciais inválidas")]
        public async Task Login_ComCredenciaisInvalidas_DeveRetornarUnauthorized()
        {
            var login = new LoginDto
            {
                Email = "admin@teste.com",
                Senha = "senhaErrada"
            };

            var response = await _client.PostAsJsonAsync("/api/authentication/login", login);

            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact(DisplayName = "Não deve autenticar com email inexistente")]
        public async Task Login_ComEmailInexistente_DeveRetornarUnauthorized()
        {
            var login = new LoginDto
            {
                Email = "naoexiste@teste.com",
                Senha = "123456"
            };

            var response = await _client.PostAsJsonAsync("/api/authentication/login", login);

            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
