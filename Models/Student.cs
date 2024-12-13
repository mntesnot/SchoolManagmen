using System.ComponentModel.DataAnnotations;

namespace SchoolManagmen.Models
{
    public class Student
    {

        [Key]
        [Display(Name ="ID No")]
        public int Id { get; set; }
        [Required]
        [Display(Name="FullName")]
        public string Name { get; set; }
        [Range(10,50,ErrorMessage ="Please Enter b/n 10 and 50")]
        public int Age { get; set; } = 0;
        public string ? PhoneNo { get; set; }

        

    }
}
