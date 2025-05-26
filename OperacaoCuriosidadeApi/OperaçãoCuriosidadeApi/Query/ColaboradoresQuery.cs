using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OperaçãoCuriosidadeApi.Context;
using OperaçãoCuriosidadeApi.Models;

namespace OperaçãoCuriosidadeApi.Query;

public class ColaboradoresQuery
{
    private readonly AppDbContext _context;
    public ColaboradoresQuery(AppDbContext context)
    {
        _context = context;
    }
    public List<Colaborador> GetAll()
    {
        var colaboradores = _context.Colaboradores.AsNoTracking().ToList();
        return colaboradores;
    }
    public Colaborador? GetById(int id)
    {
        var colaborador = _context.Colaboradores.FirstOrDefault(c => c.Id == id);
        return colaborador;
    }
}

