using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIBLOG.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    [JsonIgnore]
    public virtual ICollection<Post> IdPosts { get; set; } = new List<Post>();
}
