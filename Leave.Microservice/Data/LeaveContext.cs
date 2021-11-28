using Leave.Microservice.Model;
using Microsoft.EntityFrameworkCore;

namespace Leave.Microservice.Data
{
    public class LeaveContext: DbContext
    {
        public LeaveContext(DbContextOptions<LeaveContext> options): base(options)
        {

        }

        public DbSet<LeaveModel> Leaves { get; set; }
    }
}
