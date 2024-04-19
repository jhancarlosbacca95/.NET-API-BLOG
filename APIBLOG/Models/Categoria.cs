using System;
using System.Collections.Generic;

namespace APIBLOG.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Post> IdPosts { get; set; } = new List<Post>();
}
