﻿using System.ComponentModel.DataAnnotations;

//Eyleen Jeannethe Salinas Hernández
//Wilber Anibal Rivas Carranza

namespace L01_2022SH651_2022RC650.Models
{
    public class Usuarios
    {
        [Key]
        public int usuarioId { get; set; }
        public int rolId { get; set; }
        public string nombreUsuario { get; set; }
        public string clave { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }


    }
}
