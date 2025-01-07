using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OperaçãoCuriosidadeApi.Command;
using OperaçãoCuriosidadeApi.Context;
using OperaçãoCuriosidadeApi.Models;
using OperaçãoCuriosidadeApi.Query;
using OperaçãoCuriosidadeApi.Services;

namespace OperaçãoCuriosidadeApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UsuariosQuery _usuarioQuery;
    private readonly UsuariosCommand _usuarioCommand;
    private readonly ISecurityService _securityService;
    public UsuariosController(AppDbContext context, UsuariosQuery usuarioQuery, UsuariosCommand usuarioCommand,ISecurityService securityService)
    {
        _context = context;
        _usuarioQuery = usuarioQuery;
        _usuarioCommand = usuarioCommand;
        _securityService = securityService;
    }

    [HttpGet]
    public ActionResult<List<Usuario>> GetAll()
    {
        try
        {
            var usuarios = _usuarioQuery.GetAll();
            if (usuarios is null) return NotFound("Nenhum usuario encontrado");

            return usuarios;
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar");
        }
    }

    [HttpGet("{id:int}")]
    public ActionResult<Usuario> GetById(int id)
    {
        try
        {
            var usuario = _usuarioQuery.GetById(id);
            if (usuario is null) return NotFound("Usuario não encontrado");

            return usuario;
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar");
        }
    }

    [HttpGet("{email}")]
    public ActionResult<Usuario> GetByEmail(string email)
    {
        try
        {
            var usuario = _usuarioQuery.GetByEmail(email);
            if (usuario is null) return NotFound("Usuario não encontrado");
            return usuario;
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar");
        }
    }


    [HttpPost]
    public IActionResult Create(Usuario usuario)
    {
        try
        {
            if (usuario is null)
            {
                return BadRequest("Dados invalidos");
            }
            bool existe = _context.Usuarios.Any(u =>u.Email == usuario.Email);
            if (existe)
            {
                return BadRequest("Usuário já cadastrado");
            }

            usuario.Senha = _securityService.HashPassword(usuario.Senha);
            _usuarioCommand.Create(usuario);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar");
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Usuario usuario)
    {
        try
        {
            if (id != usuario.Id)
            {
                return BadRequest("Usuario não encontrado");
            }
            var usuarioDataBase = _context.Usuarios.AsNoTracking().FirstOrDefault(u => u.Id == id);
            if (usuarioDataBase is null) return BadRequest("usuario não encontrado");

   
            if (usuario.Senha!=usuarioDataBase.Senha)
            {
                usuario.Senha = _securityService.HashPassword(usuario.Senha);
            }
            else 
            {
                usuario.Senha= usuarioDataBase.Senha;
            
            }



            _usuarioCommand.Update(id, usuario);
                return Ok(usuario);
            
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar");

        }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario is null)
            {
                return NotFound("Usuario não enontrado");
            }
            _usuarioCommand.Delete(id);
            return Ok(usuario);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar");
        }
    }
}

