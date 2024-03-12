using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Models;

public class Cliente
{
    [Required]
    [Key]
    public int Id { get; set; }
    [Required]
    public string Nome { get; set; }
    [Required]
    public string Telefone { get; set; }
    [Required]
    [EmailAddress(ErrorMessage ="Digite um email válido!")]
    public string Email { get; set; }
    [Required]
    [DisplayName("Data Nascimento")]
    public DateTime DataNascimento { get; set; }
}
