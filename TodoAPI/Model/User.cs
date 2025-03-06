﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace TodoAPI.Model;

public class User
{
    public User()
    {
        Listas = new Collection<Lista>();
    }

    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Username é obrigatório")]
    [Length(5,24, ErrorMessage = "Username deve ter entre 5 a 24 caracteres")]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Username só deve conter Letras e Números")]
    public String Username { get; set; }

    [Required(ErrorMessage = "Senha é obrigatório")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,16}$", ErrorMessage = "Senha não segue os padrões: Entre 8 e 16 caracteres, contendo ao menos uma letras maiúscula, uma minúscula, um dígito e um caractere especial(@$!%*?&)")]
    public String Password { get; set; }

    [Required(ErrorMessage = "Confirmar senha é obrigatório")]
    [Compare(nameof(Password), ErrorMessage = "As senhas não coincidem")]
    [NotMapped]
    public String ConfirmPassword { get; set; }

    public ICollection<Lista>? Listas { get; set; }
}
