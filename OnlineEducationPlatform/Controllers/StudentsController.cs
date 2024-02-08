using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineEducationPlatform.Repository;
using OnlineEducationPlatform.Repository.Entities;

namespace OnlineEducationPlatform.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private OnlineEducationDbContext _context;
    public StudentsController(OnlineEducationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<Student>> GetAllStudent()
    {
        var students = await _context.Students.Include(s => s.Courses).Include(s => s.CourseReviews).Select(s =>new
        {
            s.Id,
            s.Name,
            s.Surname,
            s.Email,
            s.StudentId,
            Courses = s.Courses.Select(c => new{c.Id,c.Title,c.Statement}),
            Reviews = s.CourseReviews.Select(r => new{ r.Comment, r.Id, r.CourseId})
        }).ToListAsync();
        return Ok(JsonConvert.SerializeObject(students, Formatting.Indented,new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }));

    }

    [HttpGet]
    [Route("addCourse/{studentId}/{courseId}")]
    public async Task AddCourse(int studentId, int courseId)
    {
        var student =await _context.Students.Where(s => s.Id == studentId).SingleOrDefaultAsync();
        var course = await _context.Courses.Where(c => c.Id == courseId).SingleOrDefaultAsync();
        student.Courses.Add(course);
        await _context.SaveChangesAsync();
    }


    [HttpPost]
    public async Task<bool> AddStudent([FromBody]Student student)
    {
        await _context.AddAsync(student);
        return true;
    }
}