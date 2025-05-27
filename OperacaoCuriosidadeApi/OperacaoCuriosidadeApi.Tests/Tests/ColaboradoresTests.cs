using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using OperaçãoCuriosidadeApi.Models;
using OperacaoCuriosidadeApi.Tests.Helpers;
using System.Net.Http.Json;
using Xunit;

namespace OperacaoCuriosidadeApi.Tests.Tests
{
    public class ColaboradoresTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ColaboradoresTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Deve_Criar_Colaborador_Com_Sucesso()
        {
            // Arrange
            var novoColaborador = new Colaborador
            {
                Nome = "Teste",
                Email = "teste@teste.com",
                Endereco = "Rua Teste",
                Idade = "30",
                Interesses = "Tecnologia",
                OutrasInfo = "Nenhuma",
                Sentimentos = "Feliz",
                Valores = "Honestidade",
                UsuarioId = 1,
                Ativo = true
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/colaboradores", novoColaborador);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task Deve_Deletar_Colaborador_Com_Sucesso()
        {
            // Arrange
            var colaborador = new Colaborador
            {
                Nome = "Para Deletar",
                Email = "delete@teste.com",
                Endereco = "Rua Apagar",
                Idade = "25",
                Interesses = "Nada",
                OutrasInfo = "Apagar",
                Sentimentos = "Triste",
                Valores = "Nenhum",
                UsuarioId = 1,
                Ativo = true
            };

            var createResponse = await _client.PostAsJsonAsync("/api/colaboradores", colaborador);
            createResponse.EnsureSuccessStatusCode();

            var createdContent = await createResponse.Content.ReadFromJsonAsync<Colaborador>();
            var idCriado = createdContent!.Id;

            // Act
            var deleteResponse = await _client.DeleteAsync($"/api/colaboradores/{idCriado}");

            // Assert
            deleteResponse.EnsureSuccessStatusCode(); 
            deleteResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
