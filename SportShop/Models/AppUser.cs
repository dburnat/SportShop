using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SportShop.Models
{
    public class AppUser : IdentityUser
    {
        [Required] 
        public override string UserName { get; set; }
        [Required]
        [UIHint("Password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}
