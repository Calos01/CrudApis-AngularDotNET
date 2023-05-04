namespace BackMascotas.Models.Repository
{
    public interface IMascotaRepository
    {
        public Task<List<Mascota>> GetListMascota();
        public Task<Mascota> GetMascotaId(int id);
        public Task DeleteMascota(Mascota mascota);
        public Task<Mascota> AddMascota(Mascota mascota);
        public Task UpdateMascota(Mascota mascota);
    }
}
