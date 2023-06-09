﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Autor
    {

        public int Id { get; set; }
        [MaxLength(250)]
        [Required]
        public string? Nome { get; set; }
        [MaxLength(250)]
        [Required]
        public string? Sobrenome { get; set; }
        [MaxLength(250)]
        [Required]
        public string? Email { get; set; }
        public DateTime DataNascimento { get; set; }

        [JsonIgnore]
        public ICollection<Livro>? Livros { get; set; }
    }
}
