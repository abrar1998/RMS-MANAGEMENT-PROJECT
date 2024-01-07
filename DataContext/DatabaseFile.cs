using Microsoft.EntityFrameworkCore;
using RMS_Management_System.Models;

namespace RMS_Management_System.DataContext
{
    public class DatabaseFile:DbContext
    {
        public DatabaseFile(DbContextOptions<DatabaseFile> opt):base(opt)
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }  
    }
}
