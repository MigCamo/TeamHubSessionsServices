using System;
using System.Collections.Generic;

namespace TeamHubSessionsServices.Entities;

public partial class project
{
    public int IdProject { get; set; }

    public string? Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<projectdocument> projectdocument { get; set; } = new List<projectdocument>();

    public virtual ICollection<projectstudent> projectstudent { get; set; } = new List<projectstudent>();

    public virtual ICollection<projecttask> projecttask { get; set; } = new List<projecttask>();
}
