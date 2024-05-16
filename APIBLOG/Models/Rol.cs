﻿namespace APIBLOG.Models
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
