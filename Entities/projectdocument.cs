using System;
using System.Collections.Generic;

namespace TeamHubSessionsServices.Entities;

public partial class projectdocument
{
    public int IdProjectDocument { get; set; }

    public int? IdProject { get; set; }

    public int? IdDocument { get; set; }

    public virtual document? IdDocumentNavigation { get; set; }

    public virtual project? IdProjectNavigation { get; set; }
}
