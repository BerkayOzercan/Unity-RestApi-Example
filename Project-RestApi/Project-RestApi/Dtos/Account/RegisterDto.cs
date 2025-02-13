using System.ComponentModel.DataAnnotations;

namespace Project_RestApi.Dtos.Account;

public class RegisterDto
{
    [Required]
    public string? UserName { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
}
