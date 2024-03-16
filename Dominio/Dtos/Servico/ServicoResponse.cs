using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos.Servico
{
    public class ServicoResponse
    {
        
        public int Id { get; set; }
        public string NomeServico { get; set; }
        public string Descricao { get; set; }
        public int DuracaoMin { get; set; }
        public double Preco { get; set; }
    }
}
