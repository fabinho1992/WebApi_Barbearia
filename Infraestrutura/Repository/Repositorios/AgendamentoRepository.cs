using Dominio.Dtos.Agendamento;
using Dominio.Interfaces.Generic;
using Dominio.Interfaces.IService;
using Entidades.Models;
using Infraestrutura.Configuration;
using Infraestrutura.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repository.Repositorios
{
    public class AgendamentoRepository : IServiceAgendamento
    {
        private readonly IGeneric<Agendamento> _generic;
        private readonly ContextBase _context;

        public AgendamentoRepository(IGeneric<Agendamento> generic, ContextBase context)
        {
            _generic = generic;
            _context = context;
        }

        public async Task<Agendamento> GetById(int id)
        {
            var agendamento = _context.Agendamentos.Include(x => x.Cliente).Include(x => x.Servicos).FirstOrDefault(x => x.Id == id);
            return agendamento;
        }

        public async Task<IEnumerable<Agendamento>> GetAll()
        {
            var lista = await _generic.GetAll();
            return lista;
        }


        public async Task Add(Agendamento entity)
        {
            //var agendamentpRequest = new AgendamentoRequest();
            //var servico =  _context.Servicos.FirstOrDefault(x => x.Id == agendamentpRequest.ServicoId);
            //entity.Servicos = new List<Servico> { servico };
            await _generic.Add(entity);
            
        }
    }
}
