using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TodoAPI.Model;

namespace TodoAPI.DTOs.ItemDTO;

public class GetItemResponseDTO
{
    public int itemId { get; set; }
    public string titulo { get; set; }
    public string descricao { get; set; }
    [JsonPropertyName("Concluido")]
    public bool status { get; set; }
    public DateTime dataInicio { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? dataFim { get; set; }
    [JsonPropertyName("Lista")]
    public string listaTitulo { get; set; }
}
