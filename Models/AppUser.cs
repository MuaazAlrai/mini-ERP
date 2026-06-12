using Microsoft.AspNetCore.Identity;

namespace MiniERP.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}