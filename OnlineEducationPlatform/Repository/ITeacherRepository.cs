using OnlineEducationPlatform.Repository.Entities;

namespace OnlineEducationPlatform.Repository;

public interface ITeacherRepository : IRepository<Teacher>
{
    public string GetTeacherWithRelation();
}