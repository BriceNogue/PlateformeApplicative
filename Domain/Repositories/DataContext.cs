using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-ALPHA;Database=PlateformeApplicative;Trusted_Connection=true;TrustServerCertificate=true;")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}
