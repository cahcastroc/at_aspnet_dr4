using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        [MaxLength(250)]
        [Required]
        public string Nome { get; set; }
        [MaxLength(250)]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        public string Role { get; set; }
    }
}
