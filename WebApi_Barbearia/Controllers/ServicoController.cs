using AutoMapper;
using Dominio.Dtos.Cliente;
using Dominio.Dtos.Servico;
using Entidades.Models;
using Infraestrutura.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi_Barbearia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicoController : ControllerBase
    {
        private readonly ContextBase _context;
        private readonly IMapper _mapper;

        public ServicoController(ContextBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServicoRequest servicoRequest)
        {
            if (ModelState.IsValid)//para saber se as DataAnnotations foram cumpridas!
            {
                var servico = _mapper.Map<Servico>(servicoRequest);
                _context.Servicos.Add(servico);
                await _context.SaveChangesAsync();
                return Ok(servico);
            }
            return BadRequest();

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lista = await _context.Servicos.ToListAsync();
            if (lista.Count() <= 0)
            {
                return BadRequest("Nada encontrado!");
            }
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var usuario = _mapper.Map<ServicoResponse>(_context.Servicos.Find(id));
            return Ok(usuario);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, ServicoRequest ServicoRequest)
        {

            if (ModelState.IsValid)
            {
                var usuario = _context.Servicos.FirstOrDefault(c => c.Id == id);
                var servicoUpdate = _mapper.Map(ServicoRequest, usuario);
                await _context.SaveChangesAsync();
                return Ok(servicoUpdate);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            _context.Servicos.Remove(servico);
            await _context.SaveChangesAsync();
            return Ok("Usuario removido!");
        }
    }
}
