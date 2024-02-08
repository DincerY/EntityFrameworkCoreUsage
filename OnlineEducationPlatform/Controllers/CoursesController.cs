using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineEducationPlatform.Repository;
using OnlineEducationPlatform.Repository.CourseRepo;
using OnlineEducationPlatform.Repository.Entities;

namespace OnlineEducationPlatform.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICourseRepository _courseRepository;
    public CoursesController(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult GetCourse(int id)
    {
        var courseSet = _courseRepository.GetDbSet();
        var result1 = courseSet.Include(c => c.CourseReviews).Include(c => c.Students).Select(s => new
        {
            s.Title,
            s.Statement,
            Students = s.Students.Select(s => new{ s.Name, s.Surname , s.Email}),
            Teacher = new{ s.Teacher.Name, s.Teacher.Surname},
            Review = s.CourseReviews.Select(c => new{c.Comment , c.Student.Name, c.Student.Surname}),


        }).ToList();

        var result = _courseRepository.GetById(id);
        return Ok(JsonConvert.SerializeObject(result1, Formatting.Indented, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        }));

    }



}