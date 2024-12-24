using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OperaçãoCuriosidadeApi.Context;
using OperaçãoCuriosidadeApi.Models;

namespace OperaçãoCuriosidadeApi.Query;

public class UsuariosQuery
{
    private readonly AppDbContext _context;

    public UsuariosQuery(AppDbContext context)
    {
        _context = context;
    }
    public List<Usuario> GetAll()
    {
        var usuarios = _context.Usuarios.AsNoTracking().ToList();
        return usuarios;
    }
    public Usuario? GetById(int id)
    {
        var usuario = _context.Usuarios.Include(u => u.Colaboradores).FirstOrDefault(u => u.Id == id);
        return usuario;
    }

    public Usuario? GetByEmail(string email)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);
        return usuario;
    }


}

