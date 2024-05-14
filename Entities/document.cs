using System;
using System.Collections.Generic;

namespace TeamHubSessionsServices.Entities;

public partial class document
{
    public int IdDocument { get; set; }

    public string? Name { get; set; }

    public string? Ruta { get; set; }

    public virtual ICollection<projectdocument> projectdocument { get; set; } = new List<projectdocument>();
}
