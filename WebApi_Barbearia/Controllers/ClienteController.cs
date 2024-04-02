using AutoMapper;
using Dominio.Dtos.Cliente;
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
public class ClienteController : ControllerBase
{
    private readonly IServiceCliente _serviceCliente;
    private readonly IMapper _mapper;

    public ClienteController( IMapper mapper, IServiceCliente serviceCliente)
    {
        _mapper = mapper;
        _serviceCliente = serviceCliente;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var lista = await _serviceCliente.GetAll();
        var listaView = _mapper.Map<List<ClienteResponse>>(lista);
        return Ok(listaView);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClienteRequest clienteRequest)
    {
        if (ModelState.IsValid)//para saber se as DataAnnotations foram cumpridas!
        {
            var cliente = _mapper.Map<Cliente>(clienteRequest);
            await _serviceCliente.Add(cliente);
            return Ok(cliente);
        }
        return BadRequest();

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {

        var usuario = await _serviceCliente.GetById(id);
        var usuarioView = _mapper.Map<ClienteResponse>(usuario);
        return Ok( usuarioView);
    }

    [HttpPut]
    public async Task<IActionResult> Update( Cliente cliente)
    {

        if (ModelState.IsValid)
        {
            
            await _serviceCliente.Update(cliente);
            return Ok(cliente);
        }
        return BadRequest();
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        
        await _serviceCliente.Delete(id);
        return Ok("Usuario removido!");
    }

}
