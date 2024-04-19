using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIBLOG.Models;

public partial class Usuario
{
    public Guid IdUsuario { get; set; }

    public string? NombreUsuario { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Contraseña { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public bool? Activo { get; set; }

    [JsonIgnore]
    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    [JsonIgnore]
    public virtual ICollection<HistorialRefreshToken> HistorialRefreshTokens { get; set; } = new List<HistorialRefreshToken>();

    [JsonIgnore]
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
