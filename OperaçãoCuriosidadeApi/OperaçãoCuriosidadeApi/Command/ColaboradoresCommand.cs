using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OperaçãoCuriosidadeApi.Context;
using OperaçãoCuriosidadeApi.Models;

namespace OperaçãoCuriosidadeApi.Command;

public class ColaboradoresCommand
{
    private readonly AppDbContext _context;

    public ColaboradoresCommand(AppDbContext context)
    {
        _context = context;
    }

    public void Create(Colaborador colaborador)
    {
        _context.Colaboradores.Add(colaborador);
        _context.SaveChanges();
    }
    public void Update(int id, Colaborador colaborador)
    {
        _context.Colaboradores.Entry(colaborador).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var colaborador = _context.Colaboradores.FirstOrDefault(c => c.Id == id);
        if (colaborador != null)
        {
            _context.Colaboradores.Remove(colaborador);
            _context.SaveChanges();
        }
    }
}

