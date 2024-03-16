using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos.Cliente
{
    public class ClienteRequest
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Digite um email válido!")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Data Nascimento")]
        public DateTime DataNascimento { get; set; }
    }
}
