using SchoolManagmen.Models;

namespace SchoolManagmen.ViewModels
{
    public class GradeReport
    {
        public Student Student { get; set; }
        public IEnumerable<Registor> Registors { get; set; }
    }
}
