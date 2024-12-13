using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SchoolManagmen.Models
{
    public class Account : IdentityUser
    {
        [Required]
        [Display(Name ="Full Name")]
        public string Fullname { get; set; }

        [NotMapped]
        public string Password { get; set; }
    }
}
