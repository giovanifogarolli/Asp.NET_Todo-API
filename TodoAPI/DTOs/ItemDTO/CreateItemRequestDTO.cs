﻿using System.ComponentModel.DataAnnotations;

namespace TodoAPI.DTOs.ItemDTO;

public class CreateItemRequestDTO
{
    [Required(ErrorMessage = "Titulo é obrigatório.")]
    [StringLength(50)]
    public string titulo { get; set; }

    [Required(ErrorMessage = "Descrição é obrigatório.")]
    [StringLength(255)]
    public string descricao { get; set; }

    [Required]
    public int listaId { get; set; }
}
