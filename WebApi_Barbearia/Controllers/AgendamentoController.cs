using AutoMapper;
using Dominio.Dtos.Agendamento;
using Entidades.Models;
using Infraestrutura.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi_Barbearia.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgendamentoController : ControllerBase
{
    private readonly ContextBase _context;
    private readonly IMapper _mapper;

    public AgendamentoController(ContextBase context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var lista = await _context.Agendamentos.Include(c => c.Cliente).ToListAsync();
        if (lista.Count() <= 0)
        {
            return BadRequest("Nada encontrado!");
        }
        return Ok(_mapper.Map<List<AgendamentoResponse>>(lista));

    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(int id)
    {
        var agendamento = _context.Agendamentos.Include(x => x.Cliente).Include(x => x.Servicos).FirstOrDefault(x => x.Id == id);
        return Ok(_mapper.Map<AgendamentoResponse>(agendamento));
    }


    [HttpPost]
    public async Task<IActionResult> Create(AgendamentoRequest agendamentoRequest)
    {

        if (ModelState.IsValid)
        {
            var agendamento = _mapper.Map<Agendamento>(agendamentoRequest);
            var servico =  _context.Servicos.FirstOrDefault(x => x.Id == agendamentoRequest.ServicoId);
            agendamento.Servicos = new List<Servico> { servico };
            

            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();
            return Ok(agendamento);
        }
        return BadRequest();
    }

}

