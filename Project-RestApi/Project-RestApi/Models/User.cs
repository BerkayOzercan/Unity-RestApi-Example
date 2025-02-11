using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Project_RestApi.Models;

[Table("Users")]
public class User : IdentityUser
{

}
