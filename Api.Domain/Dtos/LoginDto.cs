﻿using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class LoginDto
    {
        [Required( ErrorMessage = "Email é campo obrigatório para login." )]
        [EmailAddress( ErrorMessage = "Email em formato inválido." )]
        [StringLength( 100, ErrorMessage = "Email deve ter no máximo {1} caracteres." )]
        public string? Email { get; set; }
    }
}