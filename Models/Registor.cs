using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagmen.Models
{
    public class Registor
    {
        public int Id { get; set; }
        public int StudentID  { get; set; }
        public int CourseId { get; set; }
        public float Grade { get; set; }

        public virtual  Student? Student { get; set; }
        public virtual Course? Course { get; set; }
    }
}
