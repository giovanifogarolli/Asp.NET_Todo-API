using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TodoAPI.Model;

namespace TodoAPI.DTOs.UserDTO;

public class LoginUserDTO
{
    [Required(ErrorMessage = "Username é obrigatório")]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Username só deve conter Letras e Números")]
    public String Username { get; set; }

    [Required(ErrorMessage = "Senha é obrigatório")]
    public String Password { get; set; }
}
