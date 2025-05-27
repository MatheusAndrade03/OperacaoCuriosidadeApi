
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OperaçãoCuriosidadeApi;
using OperaçãoCuriosidadeApi.Context;
using OperaçãoCuriosidadeApi.Models;
using OperaçãoCuriosidadeApi.Services;

namespace OperacaoCuriosidadeApi.Tests.Helpers
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private SqliteConnection _connection;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //Remover contexto original
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                //Criar banco SQLite em memória
                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlite(_connection);
                });

                //Reconstruir os serviços
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var securityService = scope.ServiceProvider.GetRequiredService<ISecurityService>();

                // Criar banco e dados de teste
                db.Database.EnsureCreated();

                var senhaHash = securityService.HashPassword("123456");

                db.Usuarios.Add(new Usuario
                {
                    Id = 1,
                    NomeUsuario = "Admin Teste",
                    Email = "admin@teste.com",
                    Senha = senhaHash,
                    Admin = true
                });

                db.SaveChanges();
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _connection?.Dispose();
        }
    }
}