using Microsoft.EntityFrameworkCore;

//Eyleen Jeannethe Salinas Hernández
//Wilber Anibal Rivas Carranza

namespace L01_2022SH651_2022RC650.Models
{
    public class BlogContext:DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Calificaciones> Calificaciones { get; set; }
        public DbSet<Publicaciones> Publicaciones { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
    }
}
