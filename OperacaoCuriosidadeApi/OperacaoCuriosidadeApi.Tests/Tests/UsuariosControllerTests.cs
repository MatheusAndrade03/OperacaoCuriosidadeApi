using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using OperaçãoCuriosidadeApi.Models;
using OperacaoCuriosidadeApi.Tests.Helpers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace OperacaoCuriosidadeApi.Tests
{
    public class UsuariosControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UsuariosControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Deve_Criar_Usuario_Com_Sucesso()
        {
            // Arrange
            var novoUsuario = new Usuario
            {
                NomeUsuario = "Teste Usuario",
                Email = "teste@usuario.com",
                Senha = "senha123",
                Admin = false
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/usuarios", novoUsuario);

            // Assert
            response.EnsureSuccessStatusCode();
            var usuarioCriado = await response.Content.ReadFromJsonAsync<Usuario>();
            usuarioCriado.Should().NotBeNull();
            usuarioCriado!.Email.Should().Be(novoUsuario.Email);
        }

        [Fact]
        public async Task Deve_Deletar_Usuario_Com_Sucesso()
        {
           
            var usuarioParaDeletar = new Usuario
            {
                NomeUsuario = "Deletar Usuario",
                Email = "deletar@usuario.com",
                Senha = "senha123",
                Admin = false
            };

            var postResponse = await _client.PostAsJsonAsync("/api/usuarios", usuarioParaDeletar);
            postResponse.EnsureSuccessStatusCode();
            var usuarioCriado = await postResponse.Content.ReadFromJsonAsync<Usuario>();

            // Act
            var deleteResponse = await _client.DeleteAsync($"/api/usuarios/{usuarioCriado!.Id}");

            // Assert
            deleteResponse.EnsureSuccessStatusCode();
        }
    }
}
