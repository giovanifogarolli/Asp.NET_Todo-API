using System.ComponentModel.DataAnnotations;
using TodoAPI.Model;

namespace TodoAPI.DTOs.ListaDTO;

public class CreateListaDTO
{
    [Required]
    [StringLength(maximumLength:50, MinimumLength = 5)]
    public String titulo { get; set; }
}
