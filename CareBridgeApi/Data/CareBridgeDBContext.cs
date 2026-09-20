using CareBridgeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CareBridgeApi.Data
{
    public class CareBridgeDBContext : DbContext
    {
        public CareBridgeDBContext(DbContextOptions<CareBridgeDBContext> options)
            : base(options)
        {
        }

        // this is EF Core's representation of the Patients table
        public DbSet<Patient> Patients { get; set; }
    }
}
