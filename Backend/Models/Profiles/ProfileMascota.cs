using BackMascotas.Models.DTO;

namespace BackMascotas.Models.Profiles
{
    public class ProfileMascota: AutoMapper.Profile
    {
        public ProfileMascota()
        {
            CreateMap<Mascota, MascotaDTO>();
            CreateMap<MascotaDTO, Mascota>();
        }
    }
}
