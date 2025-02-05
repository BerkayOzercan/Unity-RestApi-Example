using System;

namespace Project_RestApi.Models;

public class Player
{
    public int id { get; set; }
    public string? Name { get; set; }
    public int Rank { get; set; }
    public decimal Score { get; set; }
    public string? EMail { get; set; }
}
