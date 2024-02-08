using System;
using System.Collections.Generic;

namespace OnlineEducationPlatform.Repository.Entities;

public partial class Course
{
    public int Id { get; set; }

    public int CourseCategoryId { get; set; }

    public int TeacherId { get; set; }

    public string Title { get; set; } = null!;

    public string Statement { get; set; } = null!;

    public virtual CourseCategory CourseCategory { get; set; } = null!;

    public virtual ICollection<CourseReview> CourseReviews { get; } = new List<CourseReview>();

    public virtual Teacher Teacher { get; set; } = null!;

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
