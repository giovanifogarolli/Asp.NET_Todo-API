using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TodoAPI.Model;

public class Lista
{
    public Lista()
    {
        itens = new Collection<Item>();
    }
    [Key]
    [JsonIgnore]
    public int listaId { get; set; }

    [Required]
    [StringLength(50)]
    public String titulo { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<Item>? itens { get; set; }

}
