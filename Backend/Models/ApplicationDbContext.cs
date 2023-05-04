using Microsoft.EntityFrameworkCore;

namespace BackMascotas.Models
    
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Mascota> Mascotas { get; set; } = null!;
    }
}
