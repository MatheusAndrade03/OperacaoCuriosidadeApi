using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OperaçãoCuriosidadeApi.Context;
using OperaçãoCuriosidadeApi.Models;

namespace OperaçãoCuriosidadeApi.Command;

public class UsuariosCommand
{
    private readonly AppDbContext _context;
    public UsuariosCommand(AppDbContext context)
    {
        _context = context;
    }
    public void Create(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }
    public void Update(int id, Usuario usuario)
    {
        _context.Entry(usuario).State = EntityState.Modified;
        _context.SaveChanges();
    }
    public void Delete(int id)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
    }
}

