using Microsoft.EntityFrameworkCore;

namespace Linqgroupbymethod
{
    public class StudentDbcontext : DbContext
    {
        public DbSet<StudentInformation> stuinformation { get; set; }
        public DbSet<Records> Record { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=KARTHIK\\MSSQL;Database=student;User Id=sa;Password=Welcome@123;Encrypt=False",k=>k.UseCompatibilityLevel(120));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentInformation>().HasKey(s => s.StudentID);
            modelBuilder.Entity<Records>().HasKey(r => r.RecordID);
        }
    }
}
