using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HospitalManagement.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HospitalDbContext>
    {
        public HospitalDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HospitalDbContext>();

            // Keep design-time migrations aligned with the application's SQL Server Express instance.
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=HospitalDB;Integrated Security=True;TrustServerCertificate=True;");

            return new HospitalDbContext(optionsBuilder.Options);
        }
    }
}
