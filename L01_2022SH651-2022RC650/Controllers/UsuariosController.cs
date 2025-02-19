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
    public class UsuariosController : ControllerBase
    {
        private readonly BlogContext _blogContext;
        public UsuariosController(BlogContext blogContex)
        {
            _blogContext = blogContex;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Usuarios> listadoUsuario = (from Usuarios in _blogContext.Usuarios select Usuarios).ToList();

            if (listadoUsuario.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoUsuario);
        }



        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarUsuario([FromBody] Usuarios usuario)
        {
            try
            {
                _blogContext.Usuarios.Add(usuario);
                _blogContext.SaveChanges();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarEquipo(int id, [FromBody] Usuarios usuarioModificar)
        {
            Usuarios? usuarioActual = (from Usuarios in _blogContext.Usuarios
                                  where Usuarios.usuarioId == id
                                  select Usuarios).FirstOrDefault();

            if (usuarioActual == null)
            {
                return NotFound();
            }

            usuarioActual.rolId = usuarioModificar.rolId;
            usuarioActual.nombreUsuario = usuarioModificar.nombreUsuario;
            usuarioActual.nombre = usuarioModificar.nombre;
            usuarioActual.apellido = usuarioModificar.apellido;

            _blogContext.Entry(usuarioActual).State = EntityState.Modified;
            _blogContext.SaveChanges();

            return Ok(usuarioModificar);
        }


        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarLibro(int id)
        {
            Usuarios? usuarios = (from Usuarios in _blogContext.Usuarios
                            where Usuarios.usuarioId == id
                            select Usuarios).FirstOrDefault();

            if (usuarios == null)
            {
                return NotFound();
            }

            _blogContext.Usuarios.Attach(usuarios);
            _blogContext.Usuarios.Remove(usuarios);
            _blogContext.SaveChanges();

            return Ok(usuarios);
        }

        [HttpGet]
        [Route("GetByNombreApellido")]
        public IActionResult GetByNombreApellido(string nombre, string apellido)
        {
            var UsuarioPorNombre = (from Usuarios in _blogContext.Usuarios
                                    where Usuarios.nombre.Contains(nombre) && Usuarios.apellido.Contains(apellido)
                                    select Usuarios).ToList();


            if (UsuarioPorNombre == null)
            {
                return NotFound();
            }
            return Ok(UsuarioPorNombre);
        }

       
        [HttpGet]
        [Route("GetByRol")]
        public IActionResult GetByRol(string rol)
        {
            var UsuarioPorNombre = (from Usuarios in _blogContext.Usuarios
                                    join Roles in _blogContext.Roles on Usuarios.rolId equals Roles.rolId
                                    where Roles.rol.Contains(rol)
                                    select Usuarios).ToList();


            if (UsuarioPorNombre == null)
            {
                return NotFound();
            }
            return Ok(UsuarioPorNombre);
        }


    }
}
