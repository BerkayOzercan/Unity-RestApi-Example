using System;

namespace Project_RestApi.Models;

public class Player
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public int Score { get; set; }
    public string? EMail { get; set; }
}
