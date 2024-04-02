using Dominio.Interfaces.Generic;
using Entidades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.IService
{
    public interface IServiceAgendamento 
    {
        Task<Agendamento> GetById(int id);
        Task<IEnumerable<Agendamento>> GetAll();
        Task Add(Agendamento entity);
    }
}
