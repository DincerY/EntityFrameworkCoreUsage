using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineEducationPlatform.Repository;
using OnlineEducationPlatform.Repository.Entities;

namespace OnlineEducationPlatform.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeachersController : ControllerBase
{
    private readonly ITeacherRepository _teacherRepository;
    public TeachersController(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    [HttpGet]
    public ActionResult Get()
    {
        var teachers = _teacherRepository.GetTeacherWithRelation();
        return Ok(teachers);

    }


    [HttpGet]
    [Route("where")]
    public Teacher GetWhere()
    {
        return _teacherRepository.GetWhere(t => t.Id == 1).SingleOrDefault();
    }
}