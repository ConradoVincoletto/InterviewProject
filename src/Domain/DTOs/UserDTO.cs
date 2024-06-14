using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain.DTOs;

public class UserDTO
{
    public long UserId { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]    
    [MaxLength(255)]
    [DisplayName("FirstName")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Sobrenome é obrigatório")]
    [MaxLength(255)]
    [DisplayName("LastName")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Login é obrigatório")]
    [DisplayName("Login")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Senha é obrigatório")]
    [DisplayName("Password")]
    public string Password { get; set; }

    [Required(ErrorMessage = "E-mail é obrigatório")]
    [MaxLength(255)]
    [DisplayName("Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Idade é obrigatório")]
    [Range(1, 99)]
    [DisplayName("Age")]
    public int Age { get; set; }
}
