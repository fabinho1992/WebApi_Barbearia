using Dominio.Dtos.Servico;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos.Agendamento
{
    public class AgendamentoResponse
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Observacao { get; set; }
        public int Status { get; set; }
        public string Cliente { get; set; }
        public ICollection<ServicoResponse> Servicos { get; set; }
    }
}
