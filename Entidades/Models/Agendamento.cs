using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Models;

public class Agendamento
{
    [Required]
    [Key]
    public int Id { get; set; }
    [Required]
    public DateTime DataHora { get; set; }
    public string Observacao { get; set; }
    [Required]
    public int Status { get; set; }
    [Required]
    public int ClienteId { get; set; }
    [ForeignKey("ClienteId")]
    public Cliente Cliente { get; set; }
    public virtual ICollection<Servico> Servicos { get; set; }

}
