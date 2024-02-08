using System;
using System.Collections.Generic;

namespace OnlineEducationPlatform.Repository.Entities;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public int StudentId { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<CourseReview> CourseReviews { get; } = new List<CourseReview>();

    public virtual ICollection<Course> Courses { get; } = new List<Course>();

    public virtual ICollection<Teacher> Teachers { get; } = new List<Teacher>();
}
