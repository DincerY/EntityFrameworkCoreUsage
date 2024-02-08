using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineEducationPlatform.Repository.Entities;

namespace OnlineEducationPlatform.Repository;

public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
{
    public TeacherRepository(OnlineEducationDbContext context) : base(context)
    {
    }

    public string GetTeacherWithRelation()
    {
        var teachers = _dbSet.Include(t => t.Courses).Include(t => t.Students).Select(s => new
        {
            s.Id,
            s.Name,
            s.Surname,
            s.Email,
            s.Profession,
            Students = s.Students.Select(s => new { s.Name, s.Surname }),
            Courses = s.Courses.Select(c => new { c.Title, c.Statement })

        }).ToList();

        return JsonConvert.SerializeObject(teachers, Formatting.Indented, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        });
    }
}