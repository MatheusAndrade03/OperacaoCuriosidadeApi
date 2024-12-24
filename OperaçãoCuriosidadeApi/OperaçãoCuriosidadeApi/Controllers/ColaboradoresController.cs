using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OperaçãoCuriosidadeApi.Command;
using OperaçãoCuriosidadeApi.Context;
using OperaçãoCuriosidadeApi.Models;
using OperaçãoCuriosidadeApi.Query;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OperaçãoCuriosidadeApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ColaboradoresController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ColaboradoresQuery _colaboradorQuery;
    private readonly ColaboradoresCommand _colaboradorCommand;
    public ColaboradoresController(AppDbContext context, ColaboradoresQuery colaboradorQuery, ColaboradoresCommand colaboradorCommand)
    {
        _context = context;
        _colaboradorQuery = colaboradorQuery;
        _colaboradorCommand = colaboradorCommand;
    }

    [HttpGet]
    public ActionResult<List<Colaborador>> GetAll()
    {
        try
        {
            var colaboradores = _colaboradorQuery.GetAll();
            if (colaboradores is null)
            {
                return NotFound("Colaboradores não encontrados");
            }
            return colaboradores;
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obter  Colaboradores do banco de dados");
        }
    }

    [HttpGet("{id}", Name = "ObterColaborador")]
    public ActionResult<Colaborador> GetById(int id)
    {
        try
        {
            var colaborador = _colaboradorQuery.GetById(id);
            if (colaborador == null) return NotFound("Colaborador não enontrado");

            return colaborador;
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obter Colaborador do banco de dados");
        }
    }

    [HttpPost]
    public ActionResult Create(Colaborador colaborador)
    {
        try
        {
            if (colaborador is null)
                return NotFound("Colaborador não encontrado");

            _colaboradorCommand.Create(colaborador);
            return new CreatedAtRouteResult("ObterColaborador", new { id = colaborador.Id }, colaborador);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar");
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Colaborador colaborador)
    {
        try
        {
            if (colaborador.Id != id)
            {
                return BadRequest("Colaborador não enontrado");

            }
            _colaboradorCommand.Update(id, colaborador);
            return Ok(colaborador);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar");
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var colaborador = _context.Colaboradores.FirstOrDefault(c => c.Id == id);
            if (colaborador is null) return NotFound("Colaborador não encontrado");

            _colaboradorCommand.Delete(id);
            return Ok(colaborador);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar");
        }

    }







}

