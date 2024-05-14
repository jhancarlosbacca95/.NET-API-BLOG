using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIBLOG.Models;

public partial class Comentario
{
    public int IdComentario { get; set; }

    public Guid? IdUsuario { get; set; }

    public int? IdPost { get; set; }

    public string? Contenido { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    [JsonIgnore]
    public virtual Post? IdPostNavigation { get; set; }
    [JsonIgnore]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
