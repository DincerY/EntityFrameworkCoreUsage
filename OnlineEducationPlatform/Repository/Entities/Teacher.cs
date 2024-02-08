using System;
using System.Collections.Generic;

namespace OnlineEducationPlatform.Repository.Entities;

public partial class Teacher
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Profession { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; } = new List<Course>();

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
