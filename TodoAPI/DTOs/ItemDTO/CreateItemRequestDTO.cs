using System.ComponentModel.DataAnnotations;

namespace TodoAPI.DTOs.ItemDTO;

public class CreateItemRequestDTO
{
    [Required]
    [StringLength(50)]
    public string titulo { get; set; }

    [Required]
    [StringLength(255)]
    public string descricao { get; set; }

    [Required]
    public int listaId { get; set; }
}
