using L01_2022SH651_2022RC650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Eyleen Jeannethe Salinas Hernández
//Wilber Anibal Rivas Carranza

namespace L01_2022SH651_2022RC650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly BlogContext _blogContexto;
        public ComentariosController(BlogContext blogContexto)
        {
            _blogContexto = blogContexto;
        }

        //READ
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Comentarios> listaComentarios = (from c in _blogContexto.Comentarios select c).ToList();

            if (listaComentarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaComentarios);
        }

        //CREATE
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarComentario([FromBody] Comentarios comentario)
        {
            try
            {
                _blogContexto.Comentarios.Add(comentario);
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
        public IActionResult ActualizarComentario(int id, [FromBody] Comentarios comentarioModificar)
        {
            Comentarios? comentarioActual = (from e in _blogContexto.Comentarios where e.cometarioId == id select e).FirstOrDefault();

            if (comentarioActual == null)
            {
                return NotFound();
            }

            comentarioActual.publicacionId = comentarioModificar.publicacionId;
            comentarioActual.comentario = comentarioModificar.comentario;
            comentarioActual.usuarioId = comentarioModificar.usuarioId;

            _blogContexto.Entry(comentarioActual).State = EntityState.Modified;
            _blogContexto.SaveChanges();

            return Ok(comentarioModificar);
        }

        //Delete
        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarComentario(int id)
        {
            Comentarios? comentario = (from e in _blogContexto.Comentarios where e.cometarioId == id select e).FirstOrDefault();

            if (comentario == null)
            {
                return NotFound();
            }
            _blogContexto.Comentarios.Attach(comentario);
            _blogContexto.Comentarios.Remove(comentario);
            _blogContexto.SaveChanges();
            return Ok(comentario);
        }

        //ComentariosDeUnaPublicación
        [HttpGet]
        [Route("GetComentariosPorPublicacion/{id}")]
        public IActionResult GetComentariosPorPublicacion(int id)
        {
            var comentario = (from p in _blogContexto.Publicaciones
                               where p.publicacionId == id
                               select new
                               {
                                   p.titulo,
                                   p.descripcion,
                                   Comentarios = (from c in _blogContexto.Comentarios where c.publicacionId == id 
                                                  select c.comentario).ToList()
                               }).FirstOrDefault();


            if (comentario == null)
            {
                return NotFound();
            }
            return Ok(comentario);
        }
    }
}
