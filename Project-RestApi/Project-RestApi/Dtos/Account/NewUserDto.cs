using System;

namespace Project_RestApi.Dtos.Account;

public class NewUserDto
{
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Token { get; set; }
}
