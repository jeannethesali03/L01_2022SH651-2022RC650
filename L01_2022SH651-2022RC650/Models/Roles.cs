using System.ComponentModel.DataAnnotations;

//Eyleen Jeannethe Salinas Hernández
//Wilber Anibal Rivas Carranza

namespace L01_2022SH651_2022RC650.Models
{
    public class Roles
    {
        [Key]
        public int rolId { get; set; }
        public string rol { get; set; }

    }
}
