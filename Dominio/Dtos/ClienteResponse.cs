using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos
{
    public class ClienteResponse
    {
        public int Id { get; set; }
       
        public string Nome { get; set; }
        
        public string Telefone { get; set; }
        
        public string Email { get; set; }
        
        public DateTime DataNascimento { get; set; }

        public DateTime DataConsulta { get; set; } = DateTime.Now;
    }
}
