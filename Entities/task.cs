using System;
using System.Collections.Generic;

namespace TeamHubSessionsServices.Entities;

public partial class task
{
    public int IdTask { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<projecttask> projecttask { get; set; } = new List<projecttask>();

    public virtual ICollection<taskstudent> taskstudent { get; set; } = new List<taskstudent>();
}
