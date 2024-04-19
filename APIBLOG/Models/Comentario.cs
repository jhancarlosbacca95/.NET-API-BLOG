using System;
using System.Collections.Generic;

namespace APIBLOG.Models;

public partial class Comentario
{
    public int IdComentario { get; set; }

    public Guid? IdUsuario { get; set; }

    public int? IdPost { get; set; }

    public string? Contenido { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public virtual Post? IdPostNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
