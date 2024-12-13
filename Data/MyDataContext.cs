using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagmen.Models;

namespace SchoolManagmen.Data
{
    public class MyDataContext : IdentityDbContext<Account>
    {
        public MyDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = default;
        public DbSet<Course> Courses { get; set; } = default;
        public DbSet<Registor> Registor { get; set; } = default!;
        
    }
}
