using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HospitalManagement.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HospitalDbContext>
    {
        public HospitalDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HospitalDbContext>();

            // Assuming you're using the connection string in your app.config
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HospitalDB;Integrated Security=True;");

            return new HospitalDbContext(optionsBuilder.Options);
        }
    }
}