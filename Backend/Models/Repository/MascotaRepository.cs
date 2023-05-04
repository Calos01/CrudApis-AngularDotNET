using Microsoft.EntityFrameworkCore;

namespace BackMascotas.Models.Repository
{
    public class MascotaRepository : IMascotaRepository
    {
        private readonly ApplicationDbContext _context;
        public MascotaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Mascota> AddMascota(Mascota mascota)
        {
            _context.Add(mascota);
            await _context.SaveChangesAsync();
            return mascota;
        }

        public async Task DeleteMascota(Mascota mascota)
        {
            _context.Mascotas.Remove(mascota);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Mascota>> GetListMascota()
        {
            return await _context.Mascotas.ToListAsync();
        }

        public async Task<Mascota> GetMascotaId(int id)
        {
            var mascota = await _context.Mascotas.FindAsync(id);
            return mascota;
        }

        public async Task UpdateMascota(Mascota mascota)
        {
            var mascotaId=await _context.Mascotas.FirstOrDefaultAsync(x=>x.Id==mascota.Id);
            mascotaId.Nombre = mascota.Nombre;
            mascotaId.Raza = mascota.Raza;
            mascotaId.Edad = mascota.Edad;
            mascotaId.Peso = mascota.Peso;
            mascotaId.Color = mascota.Color;
            await _context.SaveChangesAsync();
        }
    }
}
