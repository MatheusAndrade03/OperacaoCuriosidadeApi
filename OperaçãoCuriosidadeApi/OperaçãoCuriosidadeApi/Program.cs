using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using OperaçãoCuriosidadeApi.Command;
using OperaçãoCuriosidadeApi.Context;
using OperaçãoCuriosidadeApi.Query;
using OperaçãoCuriosidadeApi.Services;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string conectionBD = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(conectionBD, ServerVersion.AutoDetect(conectionBD)));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddScoped<UsuariosQuery>();
builder.Services.AddScoped<UsuariosCommand>();
builder.Services.AddScoped<ColaboradoresQuery>();
builder.Services.AddScoped<ColaboradoresCommand>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
