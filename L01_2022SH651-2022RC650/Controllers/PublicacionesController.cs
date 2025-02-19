using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2022SH651_2022RC650.Models;
using Microsoft.EntityFrameworkCore;

//Eyleen Jeannethe Salinas Hernández
//Wilber Anibal Rivas Carranza

namespace L01_2022SH651_2022RC650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionesController : ControllerBase
    {
        private readonly BlogContext _blogContexto;

        public PublicacionesController(BlogContext blogContexto)
        {
            _blogContexto = blogContexto;
        }

        //READ
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Publicaciones> listaPublicaciones = (from p in _blogContexto.Publicaciones select p).ToList();

            if (listaPublicaciones.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaPublicaciones);
        }

        //CREATE
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPublicacion([FromBody] Publicaciones publicacion)
        {
            try
            {
                _blogContexto.Publicaciones.Add(publicacion);
                _blogContexto.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //UPDATE
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarPublicacion(int id, [FromBody] Publicaciones publicacionModificar)
        {
            Publicaciones? publicacionActual = (from e in _blogContexto.Publicaciones where e.publicacionId == id select e).FirstOrDefault();

            if (publicacionActual == null)
            {
                return NotFound();
            }

            publicacionActual.titulo = publicacionModificar.titulo;
            publicacionActual.descripcion = publicacionModificar.descripcion;
            publicacionActual.usuarioId = publicacionModificar.usuarioId;

            _blogContexto.Entry(publicacionActual).State = EntityState.Modified;
            _blogContexto.SaveChanges();

            return Ok(publicacionModificar);
        }

        //Delete
        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarPublicacion(int id)
        {
            Publicaciones? publicacion = (from e in _blogContexto.Publicaciones where e.publicacionId == id select e).FirstOrDefault();

            if (publicacion == null)
            {
                return NotFound();
            }
            _blogContexto.Publicaciones.Attach(publicacion);
            _blogContexto.Publicaciones.Remove(publicacion);
            _blogContexto.SaveChanges();
            return Ok(publicacion);
        }

        //PublicacionesPorUsuario
        [HttpGet]
        [Route("GetPublicacionesPorUsuario/{id}")]
        public IActionResult Get(int id)
        {
            var publicacion = (from a in _blogContexto.Publicaciones
                         join u in _blogContexto.Usuarios on a.usuarioId equals u.usuarioId
                         where u.usuarioId == id
                         select new
                         {
                             u.nombre,
                             u.apellido,
                             Publicaciones = (from p in _blogContexto.Publicaciones where p.usuarioId == id select p.titulo).ToList()
                         }).FirstOrDefault();


            if (publicacion == null)
            {
                return NotFound();
            }
            return Ok(publicacion);
        }


        //TopN
        [HttpGet]
        [Route("TopPublicaciones/{topN}")]
        public IActionResult TopPublicaciones(int topN)
        {
            var topPublicaciones = _blogContexto.Publicaciones
                .Select(publicaciones => new
                {
                    publicaciones.publicacionId,
                    publicaciones.titulo,
                    cantidadComentarios = _blogContexto.Comentarios
                        .Count(comentarios => comentarios.publicacionId == publicaciones.publicacionId)
                })
                .OrderByDescending(publicaciones => publicaciones.cantidadComentarios)
                .Take(topN)
                .ToList();

            if (!topPublicaciones.Any())
            {
                return NotFound();
            }

            return Ok(topPublicaciones);
        }



    }
}
