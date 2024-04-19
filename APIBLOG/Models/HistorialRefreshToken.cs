﻿using System;
using System.Collections.Generic;

namespace APIBLOG.Models;

public partial class HistorialRefreshToken
{
    public int IdHistorialToken { get; set; }

    public Guid? IdUsuario { get; set; }

    public string? Token { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaExpiracion { get; set; }

    public bool? EsActivo { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
