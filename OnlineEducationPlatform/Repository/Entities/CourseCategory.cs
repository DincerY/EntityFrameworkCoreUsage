using System;
using System.Collections.Generic;

namespace OnlineEducationPlatform.Repository.Entities;

public partial class CourseCategory
{
    public int Id { get; set; }

    public string Category { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; } = new List<Course>();
}
