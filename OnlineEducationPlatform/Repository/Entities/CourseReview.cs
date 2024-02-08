using System;
using System.Collections.Generic;

namespace OnlineEducationPlatform.Repository.Entities;

public partial class CourseReview
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public string Comment { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
