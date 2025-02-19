﻿using System.ComponentModel.DataAnnotations;

//Eyleen Jeannethe Salinas Hernández
//Wilber Anibal Rivas Carranza

namespace L01_2022SH651_2022RC650.Models
{
    public class Publicaciones
    {
        [Key]
        public int publicacionId { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int usuarioId { get; set; }

    }
}
