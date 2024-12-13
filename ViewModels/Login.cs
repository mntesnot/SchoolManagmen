using System.ComponentModel.DataAnnotations;

namespace SchoolManagmen.ViewModels
{
    public class Login
    {
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
