using Microsoft.EntityFrameworkCore;
using Payroll.Microservice.Model;

namespace Payroll.Microservice.Data
{
    public class PayrollContext: DbContext
    {
        public PayrollContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<PayrollModel> Payrolls { get; set; }
    }
}
