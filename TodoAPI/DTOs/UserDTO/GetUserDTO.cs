using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TodoAPI.Model;

namespace TodoAPI.DTOs.UserDTO;

public class GetUserDTO
{
    public int UserId { get; set; }
    public String Username { get; set; }
    public ICollection<Lista> Listas { get; set; }

}
