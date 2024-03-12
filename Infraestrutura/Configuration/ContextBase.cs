using Entidades.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Configuration
{
    public class ContextBase : DbContext
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        
    }
}
