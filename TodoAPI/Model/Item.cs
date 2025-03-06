using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TodoAPI.Model;

[Table("Item")]
public class Item
{
    [Key]
    [JsonIgnore]
    public int itemId { get; set; }

    [Required]
    [StringLength(50)]
    public String titulo { get; set; }

    [Required]
    [StringLength(255)]
    public String descricao { get; set; }
    [Required]
    public Boolean status { get; set; }
    public DateTime dataInicio { get; set; }
    public DateTime dataFim { get; set; }

    public int listaId { get; set; }
    [JsonIgnore]
    public Lista lista { get; set; }

}
