using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIBLOG.Models;

public partial class Post
{
    public int IdPost { get; set; }

    public Guid? IdUsuario { get; set; }

    public string? Titulo { get; set; }

    public string? Contenido { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public string? Imagen { get; set; }

    public bool? Activo { get; set; }
    [JsonIgnore]
    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    [JsonIgnore]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<Categoria> IdCategoria { get; set; } = new List<Categoria>();
}
