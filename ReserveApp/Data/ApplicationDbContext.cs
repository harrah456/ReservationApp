using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ReserveApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

    }
}