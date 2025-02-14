using System.ComponentModel.DataAnnotations.Schema;

namespace Project_RestApi.Models;

[Table("Players")]
public class Player
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public int Rank { get; set; }
    public decimal Score { get; set; }
    public string? UserId { get; set; }
}
