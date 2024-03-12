using AutoMapper;
using Dominio.Dtos;
using Entidades.Models;
using Infraestrutura.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi_Barbearia.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly ContextBase _context;
    private readonly IMapper _mapper;

    public ClienteController(ContextBase context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var lista = _mapper.Map<List<ClienteResponse>>(_context.Clientes.ToList());
        return Ok(lista);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClienteRequest clienteRequest)
    {
        if (ModelState.IsValid)//para saber se as DataAnnotations foram cumpridas!
        {
            var cliente = _mapper.Map<Cliente>(clienteRequest);
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return Ok(cliente);
        }
        return BadRequest();

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var usuario = _mapper.Map<ClienteResponse>(_context.Clientes.Find(id));
        return Ok(usuario);
    }

    [HttpPut]
    public async Task<IActionResult> Update(int id, ClienteRequest clienteRequest)
    {
        if (ModelState.IsValid)
        {
            var usuario = _context.Clientes.FirstOrDefault(c => c.Id == id);
            var usuarioUpdate = _mapper.Map(clienteRequest, usuario);
            await _context.SaveChangesAsync();
            return Ok(usuarioUpdate);
        }
        return BadRequest();
        
    }

}
