using System;
using System.Collections.Generic;

namespace TeamHubSessionsServices.Entities;

public partial class projecttask
{
    public int IdProjectTask { get; set; }

    public int? IdProject { get; set; }

    public int? IdTask { get; set; }

    public virtual project? IdProjectNavigation { get; set; }

    public virtual task? IdTaskNavigation { get; set; }
}
