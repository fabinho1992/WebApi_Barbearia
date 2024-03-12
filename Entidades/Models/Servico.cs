using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Models;

public class Servico
{
    [Required]
    [Key]
    public int Id { get; set; }
    [Required]
    [DisplayName("Nome Serviço")]
    public string NomeServico { get; set; }
    [DisplayName("Descrição")]
    public string Descricao { get; set; }
    [Range(10, 999, ErrorMessage = "Duração deve ter entre 10 e 999 minutos!")]
    public int DuracaoMin { get; set; }
    public double Preco { get; set; }

}
