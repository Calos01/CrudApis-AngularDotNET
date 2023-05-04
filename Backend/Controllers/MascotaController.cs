using AutoMapper;
using BackMascotas.Models;
using BackMascotas.Models.DTO;
using BackMascotas.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackMascotas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {
        
        private readonly IMapper _mapper;
        private readonly IMascotaRepository _mascotaRepository;
        public MascotaController(IMapper mapper, IMascotaRepository mascotaRepository) {
            _mapper = mapper;
            _mascotaRepository = mascotaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                //Thread.Sleep(2000);
                //var mascotitas = await _context.Mascotas.ToListAsync();
                //return Ok(mascotitas);
                //con Mapper
                var mascotitas = await _mascotaRepository.GetListMascota();
                var mascotaDTO = _mapper.Map<IEnumerable<MascotaDTO>>(mascotitas);
                return Ok(mascotaDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Con REPOSITORY
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMascota(int id)
        {
            try
            {
                var mascota = await _mascotaRepository.GetMascotaId(id);
                if (mascota == null)
                {
                    return NotFound();
                }
                //Con Mapper
                var mascotadto = _mapper.Map<MascotaDTO>(mascota);
                return Ok(mascotadto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetMascota(int id)
        //{
        //    try
        //    {
        //        var mascota = await _context.Mascotas.FindAsync(id);
        //        if (mascota == null)
        //        {
        //            return NotFound();
        //        }
        //        //Con Mapper
        //        var mascotadto = _mapper.Map<MascotaDTO>(mascota);
        //        return Ok(mascotadto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //Con REPOSITORY
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var mascota = await _mascotaRepository.GetMascotaId(id);
                if (mascota == null)
                {
                    return NotFound();
                }
                await _mascotaRepository.DeleteMascota(mascota);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        var mascota = await _context.Mascotas.FindAsync(id);
        //        if (mascota == null)
        //        {
        //            return NotFound();
        //        }
        //        _context.Mascotas.Remove(mascota);
        //        await _context.SaveChangesAsync();
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //CON REPOSITORY
        [HttpPost]
        public async Task<IActionResult> AddMascota(MascotaDTO mascotaDto)
        {
            try
            {
                var mascotaIda = _mapper.Map<Mascota>(mascotaDto);
                mascotaIda.FechaCreacion = DateTime.Now;
                mascotaIda = await _mascotaRepository.AddMascota(mascotaIda);
                var mascotaVuelta = _mapper.Map<MascotaDTO>(mascotaIda);
                //return NoContent();
                return CreatedAtAction("Get", new { id = mascotaVuelta.Id }, mascotaVuelta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //SIN MAPPER
        //[HttpPost]
        //public async Task<IActionResult> AddMascota(Mascota mascota)
        //{

        //    try
        //    {
        //        mascota.FechaCreacion = DateTime.Now;
        //        _context.Mascotas.Add(mascota);
        //        await _context.SaveChangesAsync();
        //        //return NoContent();
        //        return CreatedAtAction("Get", new {id=mascota.Id}, mascota);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}

        ////CON MAPPER
        //[HttpPost]
        //public async Task<IActionResult> AddMascota(MascotaDTO mascotaDto)
        //{

        //    try
        //    {
        //        var mascotaIda = _mapper.Map<Mascota>(mascotaDto);
        //        mascotaIda.FechaCreacion = DateTime.Now;
        //        _context.Add(mascotaIda);
        //        await _context.SaveChangesAsync();
        //        var mascotaVuelta = _mapper.Map<MascotaDTO>(mascotaIda);
        //        //return NoContent();
        //        return CreatedAtAction("Get", new {id=mascotaVuelta.Id}, mascotaVuelta);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}

        //CON REPOSITORY
        [HttpPut("{id}")]
        public async Task<IActionResult> EditMascota(int id, MascotaDTO mascotaDto)
        {
            try
            {
                var mascota = _mapper.Map<Mascota>(mascotaDto);

                var mascotaId = await _mascotaRepository.GetMascotaId(id);

                if (mascotaId == null) { return BadRequest(); }
                await _mascotaRepository.UpdateMascota(mascota);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> EditMascota(int id, MascotaDTO mascotaDto)
        //{
        //    try
        //    {
        //        var mascota = _mapper.Map<Mascota>(mascotaDto);

        //        var mascotaId = await _context.Mascotas.FindAsync(id);

        //        if (mascotaId == null) {  return BadRequest(); }
        //        mascotaId.Nombre = mascota.Nombre;
        //        mascotaId.Raza = mascota.Raza; 
        //        mascotaId.Edad = mascota.Edad;
        //        mascotaId.Peso= mascota.Peso;
        //        mascotaId.Color = mascota.Color;   
        //        await _context.SaveChangesAsync();
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
