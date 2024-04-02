using AutoMapper;
using Dominio.Interfaces.IService;
using Entidades.Models;
using Infraestrutura.Configuration;
using Infraestrutura.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repository.Repositorios
{
    public class ClienteRepository : RepositoryBase<Cliente>, IServiceCliente
    {
        public ClienteRepository(ContextBase contextBase) : base(contextBase)
        {
        }
    }
}
