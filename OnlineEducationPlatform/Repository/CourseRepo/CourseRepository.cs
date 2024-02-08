using OnlineEducationPlatform.Repository.Entities;

namespace OnlineEducationPlatform.Repository.CourseRepo;

public class CourseRepository : BaseRepository<Course>, ICourseRepository
{
    public CourseRepository(OnlineEducationDbContext context) : base(context)
    {
    }
}