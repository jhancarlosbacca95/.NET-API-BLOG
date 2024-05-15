using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIBLOG.Models;

public partial class Etiqueta
{
    public int IdEtiqueta { get; set; }

    public string? Nombre { get; set; }

    [JsonIgnore]
    public virtual ICollection<Post> IdPosts { get; set; } = new List<Post>();
}
