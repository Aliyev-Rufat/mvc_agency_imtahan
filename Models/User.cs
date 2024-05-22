using Microsoft.AspNetCore.Identity;

namespace WebApplication16.Models
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
