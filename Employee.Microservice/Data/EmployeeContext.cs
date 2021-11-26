using Employee.Microservice.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Employee.Microservice.Data
{
    public class EmployeeContext: DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options): base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            //dbContextOptionsBuilder.UseSqlServer(@"Server=HumanResource;Database=myDataBase;User Id=sa;Password=Aa123456;");
        }
        public DbSet<EmployeeModel> Employees { get; set; }
    }
}
