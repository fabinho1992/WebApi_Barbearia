using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos.Agendamento
{
    public class AgendamentoRequest
    {
        [Required]
        public DateTime DataHora { get; set; }
        public string Observacao { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public int ClienteId { get; set; }
        public int ServicoId { get; set; }
    }
}
