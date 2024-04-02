using AutoMapper;
using Dominio.Dtos.Agendamento;
using Dominio.Interfaces.IService;
using Entidades.Models;
using Infraestrutura.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi_Barbearia.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AgendamentoController : ControllerBase
{
    private readonly IServiceAgendamento _serviceAgendamento;
    private readonly IMapper _mapper;

    public AgendamentoController(IMapper mapper, IServiceAgendamento serviceAgendamento)
    {
        _mapper = mapper;
        _serviceAgendamento = serviceAgendamento;
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var lista = await _serviceAgendamento.GetAll();
        var listaMap = _mapper.Map<List<Agendamento>>(lista);
        return Ok(listaMap);

    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(int id)
    {
        var agendamento = await _serviceAgendamento.GetById(id);
        return Ok(_mapper.Map<AgendamentoResponse>(agendamento));
        //var agendamento = _context.Agendamentos.Include(x => x.Cliente).Include(x => x.Servicos).FirstOrDefault(x => x.Id == id);
        //return Ok(_mapper.Map<AgendamentoResponse>(agendamento));
    }


    [HttpPost]
    public async Task<IActionResult> Create(AgendamentoRequest agendamentoRequest)
    {

        if (ModelState.IsValid)
        {
            var agendamento = _mapper.Map<Agendamento>(agendamentoRequest);
            //var servico =  _context.Servicos.FirstOrDefault(x => x.Id == agendamentoRequest.ServicoId);
            //agendamento.Servicos = new List<Servico> { servico };
            

            await _serviceAgendamento.Add(agendamento);
            return Ok("Agendamento realizado!");
        }
        return BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Agendamento agendamento)
    {
        if (ModelState.IsValid)
        {
            //var agendamento = _context.Agendamentos.Include(x => x.Servicos).FirstOrDefault(x => x.Id == id);
            //var novoAgendamento = _mapper.Map(agendamentoRequest, agendamento);
            //var servico = _context.Servicos.FirstOrDefault(x => x.Id == agendamentoRequest.ServicoId);
            //agendamento.Servicos = new List<Servico> { servico };
            //await _context.SaveChangesAsync();
           // await _serviceAgendamento.Update(agendamento);
            return Ok("Agendamento atualizado");
        }

        return BadRequest();
        
            
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        //var agendamento = _context.Agendamentos.Include(x => x.Servicos).FirstOrDefault(x => x.Id == id);
        //if (agendamento == null) {  return BadRequest(); }
        //_context.Agendamentos.Remove(agendamento);
        //await _context.SaveChangesAsync();
        //await _serviceAgendamento.Delete(id);
        return Ok("Deletado com sucesso!");
    } 


}

