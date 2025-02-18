using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2022SH651_2022RC650.Models; 

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

    }
}
